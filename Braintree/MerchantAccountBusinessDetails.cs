#pragma warning disable 1591

using System;

namespace Braintree
{
    public interface IMerchantAccountBusinessDetails
    {
        string DbaName { get; }
        string LegalName { get; }
        string TaxId { get; }
        IAddress Address { get; }
    }

    public class MerchantAccountBusinessDetails : IMerchantAccountBusinessDetails
    {
        public string DbaName { get; protected set; }
        public string LegalName { get; protected set; }
        public string TaxId { get; protected set; }
        public IAddress Address { get; protected set; }

        protected internal MerchantAccountBusinessDetails(NodeWrapper node)
        {
            DbaName = node.GetString("dba-name");
            LegalName = node.GetString("legal-name");
            TaxId = node.GetString("tax-id");
            Address = new Address(node.GetNode("address"));
        }
    }
}
