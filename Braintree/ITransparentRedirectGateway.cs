#pragma warning disable 1591

using System;

namespace Braintree
{
    public interface ITransparentRedirectGateway
    {
        string Url { get; }

        string BuildTrData(Request request, string redirectURL);
        Result<ICreditCard> ConfirmCreditCard(string queryString);
        Result<ICustomer> ConfirmCustomer(string queryString);
        Result<ITransaction> ConfirmTransaction(string queryString);
    }
}