#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Text;

namespace Braintree
{
    /// <summary>
    /// This is the primary interface to the Braintree Gateway.
    /// </summary>
    /// <remarks>
    /// This class interact with:
    /// <ul>
    /// <li><see cref="AddressGateway">Addresses</see></li>
    /// <li><see cref="CreditCardGateway">CreditCards</see></li>
    /// <li><see cref="CustomerGateway">Customers</see></li>
    /// <li><see cref="SubscriptionGateway">Subscriptions</see></li>
    /// <li><see cref="TransactionGateway">Transactions</see></li>
    /// </ul>
    /// </remarks>
    /// <example>
    /// Quick Start Example:
    /// <code>
    /// using System;
    /// using Braintree;
    ///
    /// namespace BraintreeExample
    /// {
    ///     class Program
    ///     {
    ///         static void Main(string[] args)
    ///         {
    ///             var gateway = new BraintreeGateway
    ///             {
    ///                 Environment = Braintree.Environment.SANDBOX,
    ///                 MerchantId = "the_merchant_id",
    ///                 PublicKey = "a_public_key",
    ///                 PrivateKey = "a_private_key"
    ///             };
    ///
    ///             var request = new TransactionRequest
    ///             {
    ///                 Amount = 100.00M,
    ///                 CreditCard = new CreditCardRequest
    ///                 {
    ///                     Number = "5105105105105100",
    ///                     ExpirationDate = "05/12"
    ///                 }
    ///             };
    ///
    ///             Transaction transaction = gateway.Transaction.Sale(request).Target;
    ///
    ///             Console.WriteLine(string.Format("Transaction ID: {0}", transaction.Id));
    ///             Console.WriteLine(string.Format("Status: {0}", transaction.Status));
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public class BraintreeGateway
    {
        public Environment Environment
        {
            get { return Configuration.Environment; }
            set { Configuration.Environment = value; }
        }

        public string MerchantId
        {
            get { return Configuration.MerchantId; }
            set { Configuration.MerchantId = value; }
        }

        public string PublicKey
        {
            get { return Configuration.PublicKey; }
            set { Configuration.PublicKey = value; }
        }

        public string PrivateKey
        {
            get { return Configuration.PrivateKey; }
            set { Configuration.PrivateKey = value; }
        }

        public string AccessToken
        {
            get { return Configuration.AccessToken; }
            set { Configuration.AccessToken = value; }
        }

        public string ClientId
        {
            get { return Configuration.ClientId; }
            set { Configuration.ClientId = value; }
        }

        public string ClientSecret
        {
            get { return Configuration.ClientSecret; }
            set { Configuration.ClientSecret = value; }
        }

        public Configuration Configuration { get; set; }

        public BraintreeGateway()
        {
            Configuration = new Configuration();
        }

        public BraintreeGateway(Environment environment, string merchantId, string publicKey, string privateKey)
        {
            Configuration = new Configuration(environment, merchantId, publicKey, privateKey);
        }

        public BraintreeGateway(string accessToken)
        {
            Configuration = new Configuration(accessToken);
        }

        public BraintreeGateway(string clientId, string clientSecret)
        {
            Configuration = new Configuration(clientId, clientSecret);
        }

        public BraintreeGateway(Configuration configuration)
        {
            Configuration = configuration;
        }

        public virtual ClientTokenGateway ClientToken
        {
            get { return new ClientTokenGateway(this); }
        }

        public virtual CustomerGateway Customer
        {
            get { return new CustomerGateway(this); }
        }

        public virtual AddressGateway Address
        {
            get { return new AddressGateway(this); }
        }

        public virtual AddOnGateway AddOn
        {
            get { return new AddOnGateway(this); }
        }

        public virtual CreditCardGateway CreditCard
        {
            get { return new CreditCardGateway(this); }
        }

        public virtual CreditCardVerificationGateway CreditCardVerification
        {
            get { return new CreditCardVerificationGateway(this); }
        }

        public virtual DiscountGateway Discount
        {
            get { return new DiscountGateway(this); }
        }

        public virtual MerchantAccountGateway MerchantAccount
        {
            get { return new MerchantAccountGateway(this); }
        }

        public virtual OAuthGateway OAuth
        {
            get { return new OAuthGateway(this); }
        }

        public virtual MerchantGateway Merchant
        {
            get { return new MerchantGateway(this); }
        }

        public virtual PaymentMethodGateway PaymentMethod
        {
            get { return new PaymentMethodGateway(this); }
        }

        public virtual PayPalAccountGateway PayPalAccount
        {
            get { return new PayPalAccountGateway(this); }
        }

        public virtual PlanGateway Plan
        {
            get { return new PlanGateway(this); }
        }

        public virtual SettlementBatchSummaryGateway SettlementBatchSummary
        {
            get { return new SettlementBatchSummaryGateway(this); }
        }

        public virtual SubscriptionGateway Subscription
        {
            get { return new SubscriptionGateway(this); }
        }

        public virtual TestTransactionGateway TestTransaction
        {
            get { return new TestTransactionGateway(this); }
        }

        public virtual TransactionGateway Transaction
        {
            get { return new TransactionGateway(this); }
        }

        public virtual TransparentRedirectGateway TransparentRedirect
        {
            get { return new TransparentRedirectGateway(this); }
        }

        public virtual string TrData(Request trData, string redirectURL)
        {
            return TrUtil.BuildTrData(trData, redirectURL, new BraintreeService(Configuration));
        }

        public virtual WebhookNotificationGateway WebhookNotification
        {
            get { return new WebhookNotificationGateway(this); }
        }

        public virtual WebhookTestingGateway WebhookTesting
        {
            get { return new WebhookTestingGateway(this); }
        }

        public virtual PaymentMethodNonceGateway PaymentMethodNonce
        {
            get { return new PaymentMethodNonceGateway(this); }
        }
    }
}
