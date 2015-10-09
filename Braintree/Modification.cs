#pragma warning disable 1591

using System;

namespace Braintree
{
    public interface IModification
    {
        decimal? Amount { get; }
        DateTime? CreatedAt { get; }
        int? CurrentBillingCycle { get; }
        string Description { get; }
        string Id { get; }
        string Kind { get; }
        string MerchantId { get; }
        string Name { get; }
        bool? NeverExpires { get; }
        int? NumberOfBillingCycles { get; }
        int? Quantity { get; }
        DateTime? UpdatedAt { get; }
    }

    public class Modification : IModification
    {
        public decimal? Amount { get; protected set; }
        public DateTime? CreatedAt { get; protected set; }
        public int? CurrentBillingCycle { get; protected set; }
        public string Description { get; protected set; }
        public string Id { get; protected set; }
        public string Kind { get; protected set; }
        public string MerchantId { get; protected set; }
        public string Name { get; protected set; }
        public bool? NeverExpires { get; protected set; }
        public int? NumberOfBillingCycles { get; protected set; }
        public int? Quantity { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected Modification(NodeWrapper node)
        {
            Amount = node.GetDecimal("amount");
            CreatedAt = node.GetDateTime("created-at");
            CurrentBillingCycle = node.GetInteger("current-billing-cycle");
            Description = node.GetString("description");
            Id = node.GetString("id");
            Kind = node.GetString("kind");
            MerchantId = node.GetString("merchant-id");
            Name = node.GetString("name");
            NeverExpires = node.GetBoolean("never-expires");
            NumberOfBillingCycles = node.GetInteger("number-of-billing-cycles");
            Quantity = node.GetInteger("quantity");
            UpdatedAt = node.GetDateTime("updated-at");
        }
    }
}
