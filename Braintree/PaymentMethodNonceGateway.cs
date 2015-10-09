using System;
using Braintree.Exceptions;

namespace Braintree
{
    public class PaymentMethodNonceGateway : IPaymentMethodNonceGateway
    {
        private readonly BraintreeService service;
        private readonly BraintreeGateway gateway;

        public PaymentMethodNonceGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            this.gateway = gateway;
            this.service = new BraintreeService(gateway.Configuration);
        }

        public Result<IPaymentMethodNonce> Create(string token)
        {
            var response = new NodeWrapper(service.Post(service.MerchantPath() + "/payment_methods/" + token + "/nonces"));

            return new ResultImpl<IPaymentMethodNonce>(response, gateway);
        }

        public virtual IPaymentMethodNonce Find(string nonce)
        {
            var response = new NodeWrapper(service.Get(service.MerchantPath() + "/payment_method_nonces/" + nonce));

            return new PaymentMethodNonce(response, gateway);
        }

    }
}
