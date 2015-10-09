using System;

namespace Braintree
{
    public interface IMerchantAccountGateway
    {
        Result<IMerchantAccount> Create(MerchantAccountRequest request);
        IMerchantAccount Find(string id);
        Result<IMerchantAccount> Update(string id, MerchantAccountRequest request);
    }
}