#pragma warning disable 1591

using System;

namespace Braintree
{
    /// <summary>
    /// Provides test operations for settling, settlement confirming, settlement pending, and settlement declining for transactions in the sandbox vault
    /// </summary>
    public interface ITestTransactionGateway
    {
        ITransaction Settle(string id);
        ITransaction SettlementConfirm(string id);
        ITransaction SettlementDecline(string id);
        ITransaction SettlementPending(string id);
    }
}