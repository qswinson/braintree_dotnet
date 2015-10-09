#pragma warning disable 1591

using System;

namespace Braintree
{
    /// <summary>
    /// Provides operations for creating, finding, updating, and deleting credit cards in the vault
    /// </summary>
    public interface ICreditCardGateway
    {
        Result<ICreditCard> ConfirmTransparentRedirect(string queryString);
        Result<CreditCard> Create(CreditCardRequest request);
        void Delete(string token);
        ResourceCollection<ICreditCard> Expired();
        ResourceCollection<ICreditCard> ExpiringBetween(DateTime start, DateTime end);
        CreditCard Find(string token);
        ICreditCard FromNonce(string nonce);
        string TransparentRedirectURLForCreate();
        string TransparentRedirectURLForUpdate();
        Result<ICreditCard> Update(string token, CreditCardRequest request);
    }
}