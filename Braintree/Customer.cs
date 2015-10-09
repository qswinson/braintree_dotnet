#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Braintree
{
    public interface ICustomer
    {
        string Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Company { get; }
        string Email { get; }
        string Phone { get; }
        string Fax { get; }
        string Website { get; }
        DateTime? CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        ICreditCard[] CreditCards { get; }
        PayPalAccount[] PayPalAccounts { get; }
        IApplePayCard[] ApplePayCards { get; }
        IAndroidPayCard[] AndroidPayCards { get; }
        CoinbaseAccount[] CoinbaseAccounts { get; }
        PaymentMethod[] PaymentMethods { get; }
        IAddress[] Addresses { get; }
        Dictionary<string, string> CustomFields { get; }
        PaymentMethod DefaultPaymentMethod { get; }
    }

    /// <summary>
    /// A customer returned by the Braintree Gateway
    /// </summary>
    /// <example>
    /// Customers can be retrieved via the gateway using the associated customer id:
    /// <code>
    ///     Customer customer = gateway.Customer.Find("customerId");
    /// </code>
    /// For more information about Customers, see <a href="http://www.braintreepayments.com/gateway/customer-api" target="_blank">http://www.braintreepaymentsolutions.com/gateway/customer-api</a>
    /// </example>
    public class Customer : ICustomer
    {
        public string Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Company { get; protected set; }
        public string Email { get; protected set; }
        public string Phone { get; protected set; }
        public string Fax { get; protected set; }
        public string Website { get; protected set; }
        public DateTime? CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public ICreditCard[] CreditCards { get; protected set; }
        public PayPalAccount[] PayPalAccounts { get; protected set; }
        public IApplePayCard[] ApplePayCards { get; protected set; }
        public IAndroidPayCard[] AndroidPayCards { get; protected set; }
        public CoinbaseAccount[] CoinbaseAccounts { get; protected set; }
        public PaymentMethod[] PaymentMethods { get; protected set; }
        public IAddress[] Addresses { get; protected set; }
        public Dictionary<string, string> CustomFields { get; protected set; }
        public PaymentMethod DefaultPaymentMethod
        {
            get
            {
                foreach (PaymentMethod paymentMethod in PaymentMethods)
                {
                    if (paymentMethod.IsDefault.Value)
                    {
                        return paymentMethod;
                    }
                }
                return null;
            }
        }

        protected internal Customer(NodeWrapper node, BraintreeGateway gateway)
        {
            if (node == null) return;

            Id = node.GetString("id");
            FirstName = node.GetString("first-name");
            LastName = node.GetString("last-name");
            Company = node.GetString("company");
            Email = node.GetString("email");
            Phone = node.GetString("phone");
            Fax = node.GetString("fax");
            Website = node.GetString("website");
            CreatedAt = node.GetDateTime("created-at");
            UpdatedAt = node.GetDateTime("updated-at");

            var creditCardXmlNodes = node.GetList("credit-cards/credit-card");
            CreditCards = new CreditCard[creditCardXmlNodes.Count];
            for (int i = 0; i < creditCardXmlNodes.Count; i++)
            {
                CreditCards[i] = new CreditCard(creditCardXmlNodes[i], gateway);
            }

            var paypalXmlNodes = node.GetList("paypal-accounts/paypal-account");
            PayPalAccounts = new PayPalAccount[paypalXmlNodes.Count];
            for (int i = 0; i < paypalXmlNodes.Count; i++)
            {
                PayPalAccounts[i] = new PayPalAccount(paypalXmlNodes[i], gateway);
            }

            var applePayXmlNodes = node.GetList("apple-pay-cards/apple-pay-card");
            ApplePayCards = new ApplePayCard[applePayXmlNodes.Count];
            for (int i = 0; i < applePayXmlNodes.Count; i++)
            {
                ApplePayCards[i] = new ApplePayCard(applePayXmlNodes[i], gateway);
            }

            var androidPayCardXmlNodes = node.GetList("android-pay-cards/android-pay-card");
            AndroidPayCards = new AndroidPayCard[androidPayCardXmlNodes.Count];
            for (int i = 0; i < androidPayCardXmlNodes.Count; i++)
            {
                AndroidPayCards[i] = new AndroidPayCard(androidPayCardXmlNodes[i], gateway);
            }

            var coinbaseXmlNodes = node.GetList("coinbase-accounts/coinbase-account");
            CoinbaseAccounts = new CoinbaseAccount[coinbaseXmlNodes.Count];
            for (int i = 0; i < coinbaseXmlNodes.Count; i++)
            {
                CoinbaseAccounts[i] = new CoinbaseAccount(coinbaseXmlNodes[i], gateway);
            }

            PaymentMethods = new PaymentMethod[CreditCards.Length + PayPalAccounts.Length + ApplePayCards.Length + CoinbaseAccounts.Length + AndroidPayCards.Length];
            CreditCards.CopyTo(PaymentMethods, 0);
            PayPalAccounts.CopyTo(PaymentMethods, CreditCards.Length);
            ApplePayCards.CopyTo(PaymentMethods, CreditCards.Length + PayPalAccounts.Length);
            CoinbaseAccounts.CopyTo(PaymentMethods, CreditCards.Length + PayPalAccounts.Length + ApplePayCards.Length);
            AndroidPayCards.CopyTo(PaymentMethods, CreditCards.Length + PayPalAccounts.Length + ApplePayCards.Length + CoinbaseAccounts.Length);

            var addressXmlNodes = node.GetList("addresses/address");
            Addresses = new Address[addressXmlNodes.Count];
            for (int i = 0; i < addressXmlNodes.Count; i++)
            {
                Addresses[i] = new Address(addressXmlNodes[i]);
            }

            CustomFields = node.GetDictionary("custom-fields");
        }
    }
}
