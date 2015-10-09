#pragma warning disable 1591

using System;

namespace Braintree
{
    /// <summary>
    /// Provides operations for sales, credits, refunds, voids, submitting for settlement, and searching for transactions in the vault
    /// </summary>
    public interface ITransactionGateway
    {
        Result<ITransaction> CancelRelease(string id);
        Result<ITransaction> CloneTransaction(string id, TransactionCloneRequest cloneRequest);
        Result<ITransaction> ConfirmTransparentRedirect(string queryString);
        Result<ITransaction> Credit(TransactionRequest request);
        string CreditTrData(TransactionRequest trData, string redirectURL);
        Transaction Find(string id);
        Result<ITransaction> HoldInEscrow(string id);
        Result<Transaction> Refund(string id);
        Result<ITransaction> Refund(string id, decimal amount);
        Result<ITransaction> ReleaseFromEscrow(string id);
        Result<ITransaction> Sale(TransactionRequest request);
        string SaleTrData(TransactionRequest trData, string redirectURL);
        ResourceCollection<ITransaction> Search(TransactionSearchRequest query);
        Result<Transaction> SubmitForPartialSettlement(string id, decimal amount);
        Result<ITransaction> SubmitForSettlement(string id);
        Result<ITransaction> SubmitForSettlement(string id, decimal amount);
        string TransparentRedirectURLForCreate();
        Result<ITransaction> Void(string id);
    }
}