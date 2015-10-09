using System;

namespace Braintree
{
    public interface ITransactionDetails
    {
        string Id { get; }
        string Amount { get; }
    }

    public class TransactionDetails : ITransactionDetails
    {
        public string Id { get; protected set; }
        public string Amount { get; protected set; }

        protected internal TransactionDetails(NodeWrapper node)
        {
            Id = node.GetString("id");
            Amount = node.GetString("amount");
        }
    }
}

