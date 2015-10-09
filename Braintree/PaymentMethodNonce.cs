using System;

namespace Braintree
{
    public interface IPaymentMethodNonce
    {
        string Nonce { get; }
        string Type { get; }
        IThreeDSecureInfo ThreeDSecureInfo { get; }
    }

    public class PaymentMethodNonce : IPaymentMethodNonce
    {
        public string Nonce { get; protected set; }
        public string Type { get; protected set; }
        public IThreeDSecureInfo ThreeDSecureInfo { get; protected set; }

        protected internal PaymentMethodNonce(NodeWrapper node, BraintreeGateway gateway)
        {
            Nonce = node.GetString("nonce");
            Type = node.GetString("type");

            var threeDSecureInfoNode = node.GetNode("three-d-secure-info");
            if (threeDSecureInfoNode != null && !threeDSecureInfoNode.IsEmpty()){
                ThreeDSecureInfo = new ThreeDSecureInfo(threeDSecureInfoNode);
            }
        }
    }
}
