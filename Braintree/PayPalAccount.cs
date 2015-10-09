using System;

namespace Braintree
{
    public interface IPayPalAccount : PaymentMethod
    {
        string Email { get; }
        string BillingAgreementId { get; }
        DateTime? CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        ISubscription[] Subscriptions { get; }
    }

    public class PayPalAccount : IPayPalAccount
    {
        public string Email { get; protected set; }
        public string BillingAgreementId { get; protected set; }
        public string Token { get; protected set; }
        public bool? IsDefault { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string CustomerId { get; protected set; }
        public DateTime? CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public ISubscription[] Subscriptions { get; protected set; }

        protected internal PayPalAccount(NodeWrapper node, BraintreeGateway gateway)
        {
            Email = node.GetString("email");
            BillingAgreementId = node.GetString("billing-agreement-id");
            Token = node.GetString("token");
            IsDefault = node.GetBoolean("default");
            ImageUrl = node.GetString("image-url");
            CustomerId = node.GetString("customer-id");
            CreatedAt = node.GetDateTime("created-at");
            UpdatedAt = node.GetDateTime("updated-at");

            var subscriptionXmlNodes = node.GetList("subscriptions/subscription");
            Subscriptions = new Subscription[subscriptionXmlNodes.Count];
            for (int i = 0; i < subscriptionXmlNodes.Count; i++)
            {
                Subscriptions[i] = new Subscription(subscriptionXmlNodes[i], gateway);
            }
        }

    }
}
