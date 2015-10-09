using System;

namespace Braintree
{
    public interface IPaymentMethodNonceGateway
    {
        Result<IPaymentMethodNonce> Create(string token);
        IPaymentMethodNonce Find(string nonce);
    }
}