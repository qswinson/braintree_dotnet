using System;
using NUnit.Framework;

namespace Braintree.Tests
{
    //NOTE: good
    [TestFixture]
    public class TransparentRedirectTest
    {
        private BraintreeGateway gateway;
        private BraintreeService service;

        [SetUp]
        public void Setup()
        {
            gateway = new BraintreeGateway();
            service = new BraintreeService(gateway.Configuration);
        }

        [Test]
        public void Url_ReturnsCorrectUrl()
        {
            var u = new Uri(Environment.CONFIGURED.GatewayURL);
            var cfg = new Configuration();
            var url = u.Scheme + "://" + u.Host + ":" + u.Port + "/merchants/" + cfg.MerchantId + "/transparent_redirect_requests";
            Assert.AreEqual(url, gateway.TransparentRedirect.Url);
        }

        [Test]
        public void BuildTrData_BuildsAQueryStringWithApiVersion()
        {
            string tr_data = gateway.TransparentRedirect.BuildTrData(new TransactionRequest(), "example.com");
            TestHelper.AssertIncludes("api_version=4", tr_data);
        }

        [Test]
        public void CreateTransactionFromTransparentRedirect()
        {
            TransactionRequest trParams = new TransactionRequest
            {
                Type = TransactionType.SALE
            };

            TransactionRequest request = new TransactionRequest
            {
                Amount = SandboxValues.TransactionAmount.AUTHORIZE,
                CreditCard = new TransactionCreditCardRequest
                {
                    Number = SandboxValues.CreditCardNumber.VISA,
                    ExpirationDate = "05/2099",
                    CVV = "123",
                },
                BillingAddress = new AddressRequest
                {
                    StreetAddress = "123 fake st",
                    PostalCode = "90025",
                },
            };

            string queryString = TestHelper.QueryStringForTR(trParams, request, gateway.TransparentRedirect.Url, service);
            Result<Transaction> result = gateway.TransparentRedirect.ConfirmTransaction(queryString);
            Assert.IsTrue(result.IsSuccess(), result.Message);
            Transaction transaction = result.Target;

            Assert.AreEqual(1000.00, transaction.Amount);
            Assert.AreEqual(TransactionType.SALE, transaction.Type);
            Assert.AreEqual(TransactionStatus.AUTHORIZED, transaction.Status);
            Assert.AreEqual(DateTime.Now.Year, transaction.CreatedAt.Value.Year);
            Assert.AreEqual(DateTime.Now.Year, transaction.UpdatedAt.Value.Year);

            CreditCard creditCard = transaction.CreditCard;
            Assert.AreEqual("411111", creditCard.Bin);
            Assert.AreEqual("1111", creditCard.LastFour);
            Assert.AreEqual("05", creditCard.ExpirationMonth);
            Assert.AreEqual("2099", creditCard.ExpirationYear);
            Assert.AreEqual("05/2099", creditCard.ExpirationDate);
        }

        [Test]
        public void CreateCustomerFromTransparentRedirect()
        {
            CustomerRequest trParams = new CustomerRequest
            {
                FirstName = "John"
            };

            CustomerRequest request = new CustomerRequest
            {
                LastName = "Doe"
            };

            string queryString = TestHelper.QueryStringForTR(trParams, request, gateway.TransparentRedirect.Url, service);
            Result<ICustomer> result = gateway.TransparentRedirect.ConfirmCustomer(queryString);
            Assert.IsTrue(result.IsSuccess());
            ICustomer customer = result.Target;

            Assert.AreEqual("John", customer.FirstName);
            Assert.AreEqual("Doe", customer.LastName);
        }

        [Test]
        public void UpdateCustomerFromTransparentRedirect()
        {
            var createRequest = new CustomerRequest
            {
                FirstName = "Miranda",
                LastName = "Higgenbottom"
            };

            ICustomer createdCustomer = gateway.Customer.Create(createRequest).Target;

            CustomerRequest trParams = new CustomerRequest
            {
                CustomerId = createdCustomer.Id,
                FirstName = "Penelope"
            };

            CustomerRequest request = new CustomerRequest
            {
                LastName = "Lambert"
            };

            string queryString = TestHelper.QueryStringForTR(trParams, request, gateway.TransparentRedirect.Url, service);
            Result<ICustomer> result = gateway.TransparentRedirect.ConfirmCustomer(queryString);
            Assert.IsTrue(result.IsSuccess());
            ICustomer updatedCustomer = gateway.Customer.Find(createdCustomer.Id);

            Assert.AreEqual("Penelope", updatedCustomer.FirstName);
            Assert.AreEqual("Lambert", updatedCustomer.LastName);
        }

        [Test]
        public void CreateCreditCardFromTransparentRedirect()
        {
            ICustomer customer = gateway.Customer.Create(new CustomerRequest()).Target;
            CreditCardRequest trParams = new CreditCardRequest
            {
                CustomerId = customer.Id,
                Number = "4111111111111111",
                ExpirationDate = "10/10"
            };

            CreditCardRequest request = new CreditCardRequest
            {
                CardholderName = "John Doe"
            };

            string queryString = TestHelper.QueryStringForTR(trParams, request, gateway.TransparentRedirect.Url, service);
            Result<CreditCard> result = gateway.TransparentRedirect.ConfirmCreditCard(queryString);
            Assert.IsTrue(result.IsSuccess());
            CreditCard creditCard = result.Target;

            Assert.AreEqual("John Doe", creditCard.CardholderName);
            Assert.AreEqual("411111", creditCard.Bin);
            Assert.AreEqual("1111", creditCard.LastFour);
            Assert.AreEqual("10/2010", creditCard.ExpirationDate);
        }


        [Test]
        public void UpdateCreditCardFromTransparentRedirect()
        {
            ICustomer customer = gateway.Customer.Create(new CustomerRequest()).Target;

            var creditCardRequest = new CreditCardRequest
            {
                CustomerId = customer.Id,
                Number = "5555555555554444",
                ExpirationDate = "05/22",
                CardholderName = "Beverly D'angelo"
            };

            CreditCard createdCreditCard = gateway.CreditCard.Create(creditCardRequest).Target;


            CreditCardRequest trParams = new CreditCardRequest
            {
                PaymentMethodToken = createdCreditCard.Token,
                Number = "4111111111111111",
                ExpirationDate = "10/10"
            };

            CreditCardRequest request = new CreditCardRequest
            {
                CardholderName = "Sampsonite"
            };

            string queryString = TestHelper.QueryStringForTR(trParams, request, gateway.TransparentRedirect.Url, service);
            Result<CreditCard> result = gateway.TransparentRedirect.ConfirmCreditCard(queryString);
            Assert.IsTrue(result.IsSuccess());
            CreditCard creditCard = gateway.CreditCard.Find(createdCreditCard.Token);

            Assert.AreEqual("Sampsonite", creditCard.CardholderName);
            Assert.AreEqual("411111", creditCard.Bin);
            Assert.AreEqual("1111", creditCard.LastFour);
            Assert.AreEqual("10/2010", creditCard.ExpirationDate);
        }
    }
}
