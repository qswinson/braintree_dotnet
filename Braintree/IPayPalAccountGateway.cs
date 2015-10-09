using System;

namespace Braintree
{
    public interface IPayPalAccountGateway
    {
        void Delete(string token);
        PayPalAccount Find(string token);
        Result<IPayPalAccount> Update(string token, PayPalAccountRequest request);
    }
}