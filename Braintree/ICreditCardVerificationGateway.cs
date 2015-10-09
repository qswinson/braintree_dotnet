#pragma warning disable 1591

using System;

namespace Braintree
{
    /// <summary>
    /// Provides operations for finding verifications
    /// </summary>
    public interface ICreditCardVerificationGateway
    {
        ICreditCardVerification Find(string Id);
        ResourceCollection<ICreditCardVerification> Search(CreditCardVerificationSearchRequest query);
    }
}