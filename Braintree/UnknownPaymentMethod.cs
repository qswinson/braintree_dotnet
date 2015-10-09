using System;

namespace Braintree
{
    public interface IUnknownPaymentMethod : PaymentMethod
    {
    }

    public class UnknownPaymentMethod : IUnknownPaymentMethod
    {
        public string Token { get; protected set; }
        public bool? IsDefault { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string CustomerId { get; protected set; }

        public UnknownPaymentMethod(NodeWrapper node)
        {
            Token = node.GetString("token");
            IsDefault = node.GetBoolean("default");
            ImageUrl = "https://assets.braintreegateway.com/payment_method_logo/unknown.png";
            CustomerId = node.GetString("customer-id");
        }

        public UnknownPaymentMethod()
        {
        }
    }
}
