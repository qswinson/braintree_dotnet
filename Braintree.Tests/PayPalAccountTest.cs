using System;
using System.Linq;
using NUnit.Framework;
using Braintree.Exceptions;
using Params = System.Collections.Generic.Dictionary<string, object>;

namespace Braintree.Tests
{
    //NOTE: good
    [TestFixture]
    public class PayPalAccountTest
    {
        private BraintreeGateway gateway;

        [SetUp]
        public void Setup()
        {
            gateway = new BraintreeGateway();
        }

        [Test]
        public void Find_ReturnsPayPalAccountByToken()
        {
            Result<Customer> customerResult = gateway.Customer.Create(new CustomerRequest());
            Assert.IsTrue(customerResult.IsSuccess());

            var request = new PaymentMethodRequest
            {
                CustomerId = customerResult.Target.Id,
                PaymentMethodNonce = Nonce.PayPalFuturePayment
            };
            Result<PaymentMethod> result = gateway.PaymentMethod.Create(request);

            Assert.IsTrue(result.IsSuccess());
            Assert.IsNotNull(result.Target.ImageUrl);

            PayPalAccount found = gateway.PayPalAccount.Find(result.Target.Token);
            Assert.IsNotNull(found);
            Assert.IsNotNull(found.Email);
            Assert.IsNotNull(found.ImageUrl);
            Assert.IsNotNull(found.CreatedAt);
            Assert.IsNotNull(found.UpdatedAt);
            Assert.AreEqual(found.Email, ((PayPalAccount) result.Target).Email);
        }

        [Test]
        public void Find_RaisesNotFoundErrorForUnknownToken()
        {
            try {
                gateway.PayPalAccount.Find(" ");
                Assert.Fail("Should throw NotFoundException");
            } catch (NotFoundException) {}
        }

        [Test]
        public void Find_RaisesNotFoundErrorForCreditCardToken()
        {
            var createRequest = new CustomerRequest
            {
                CreditCard = new CreditCardRequest
                {
                    Number = "5555555555554444",
                    ExpirationDate = "05/12",
                    CVV = "123",
                }
            };

            var resp = gateway.Customer.Create(createRequest);
            Assert.IsNotNull(resp);
            ICustomer customer = resp.Target;
            Assert.IsNotNull(customer, resp.Message);
            try {
                gateway.PayPalAccount.Find(customer.CreditCards[0].Token);
                Assert.Fail("Should throw NotFoundException");
            } catch (NotFoundException) {}
        }

        [Test]
        public void Delete_ReturnsPayPalAccountByToken()
        {
            Result<Customer> customerResult = gateway.Customer.Create(new CustomerRequest());
            Assert.IsTrue(customerResult.IsSuccess());

            var request = new PaymentMethodRequest
            {
                CustomerId = customerResult.Target.Id,
                PaymentMethodNonce = Nonce.PayPalFuturePayment
            };
            Result<PaymentMethod> result = gateway.PaymentMethod.Create(request);

            Assert.IsTrue(result.IsSuccess());

            gateway.PayPalAccount.Delete(result.Target.Token);
        }

        [Test]
        public void Delete_RaisesNotFoundErrorForUnknownToken()
        {
            try {
                gateway.PayPalAccount.Delete(" ");
                Assert.Fail("Should throw NotFoundException");
            } catch (NotFoundException) {}
        }

        [Test]
        public void Update_CanUpdateToken()
        {
            Result<Customer> customerResult = gateway.Customer.Create(new CustomerRequest());
            Assert.IsTrue(customerResult.IsSuccess());

            var request = new PaymentMethodRequest
            {
                CustomerId = customerResult.Target.Id,
                PaymentMethodNonce = Nonce.PayPalFuturePayment
            };
            Result<PaymentMethod> result = gateway.PaymentMethod.Create(request);

            Assert.IsTrue(result.IsSuccess());

            string newToken = Guid.NewGuid().ToString();
            var updateRequest = new PayPalAccountRequest
            {
                Token = newToken
            };
            var updateResult = gateway.PayPalAccount.Update(result.Target.Token, updateRequest);

            Assert.IsTrue(updateResult.IsSuccess());
            Assert.AreEqual(newToken, updateResult.Target.Token);
        }

        [Test]
        public void Update_CanMakeDefault()
        {
            Result<Customer> customerResult = gateway.Customer.Create(new CustomerRequest());
            Assert.IsTrue(customerResult.IsSuccess());

            var creditCardRequest = new CreditCardRequest
            {
                CustomerId = customerResult.Target.Id,
                Number = "5555555555554444",
                ExpirationDate = "05/22"
            };
            ICreditCard creditCard = gateway.CreditCard.Create(creditCardRequest).Target;
            Assert.IsTrue(creditCard.IsDefault.Value);

            var request = new PaymentMethodRequest
            {
                CustomerId = customerResult.Target.Id,
                PaymentMethodNonce = Nonce.PayPalFuturePayment
            };
            Result<PaymentMethod> result = gateway.PaymentMethod.Create(request);

            Assert.IsTrue(result.IsSuccess());

            var updateRequest = new PayPalAccountRequest
            {
                Options = new PayPalOptionsRequest
                {
                    MakeDefault = true
                }
            };
            var updateResult = gateway.PayPalAccount.Update(result.Target.Token, updateRequest);

            Assert.IsTrue(updateResult.IsSuccess());
            Assert.IsTrue(updateResult.Target.IsDefault.Value);
        }

        [Ignore("Need to setup payment plans")]
        [Test]
        public void ReturnsSubscriptionsAssociatedWithPayPalAccount()
        {
            var customer = gateway.Customer.Create().Target;
            var paymentMethodToken = string.Format("paypal-account-{0}", DateTime.Now.Ticks);
            var nonce = TestHelper.GetNonceForPayPalAccount(
                gateway,
                new Params
                {
                    { "consent_code", "consent-code" },
                    { "token", paymentMethodToken }
                });

            var result = gateway.PaymentMethod.Create(new PaymentMethodRequest
            {
                PaymentMethodNonce = nonce,
                CustomerId = customer.Id
            });

            Assert.IsTrue(result.IsSuccess());
            var token = result.Target.Token;
            var resp1 = gateway.Subscription.Create(new SubscriptionRequest
            {
                PaymentMethodToken = token,
                PlanId = PlanFixture.PLAN_WITHOUT_TRIAL.Id
            });
            Assert.IsNotNull(resp1);
            var subscription1 = resp1.Target;
            Assert.IsNotNull(subscription1, resp1.Message);

            var resp2 = gateway.Subscription.Create(new SubscriptionRequest
            {
                PaymentMethodToken = token,
                PlanId = PlanFixture.PLAN_WITHOUT_TRIAL.Id
            });
            Assert.IsNotNull(resp2);
            var subscription2 = resp2.Target;
            Assert.IsNotNull(subscription2, resp2.Message);

            var paypalAccount = gateway.PayPalAccount.Find(token);
            Assert.IsNotNull(paypalAccount);
            var ids = from s in new Subscription[] { subscription1, subscription2 } orderby s.Id select s.Id;
            var accountSubscriptionIds = from s in paypalAccount.Subscriptions orderby s.Id select s.Id;
            Assert.IsTrue(ids.SequenceEqual(accountSubscriptionIds));
        }

        [Test]
        public void ReturnsBillingAgreementIdWithPayPalAccount()
        {
            var customer = gateway.Customer.Create().Target;
            var result = gateway.PaymentMethod.Create(new PaymentMethodRequest
            {
                PaymentMethodNonce = Nonce.PayPalBillingAgreement,
                CustomerId = customer.Id
            });

            Assert.IsTrue(result.IsSuccess());
            Assert.IsFalse(string.IsNullOrEmpty(result.Target.Token));

            var paypalAccount = gateway.PayPalAccount.Find(result.Target.Token);
            Assert.IsNotNull(paypalAccount.BillingAgreementId);
        }
    }
}
