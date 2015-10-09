using System;

namespace Braintree
{
    public class FundingDestination : Enumeration
    {
        public static readonly FundingDestination BANK = new FundingDestination("bank");
        public static readonly FundingDestination MOBILE_PHONE = new FundingDestination("mobile_phone");
        public static readonly FundingDestination EMAIL = new FundingDestination("email");
        public static readonly FundingDestination UNRECOGNIZED = new FundingDestination("unrecognized");

        public static readonly FundingDestination[] ALL = { BANK, MOBILE_PHONE, EMAIL, UNRECOGNIZED };

        protected FundingDestination(string name) : base(name) {}
    }

    public class MerchantAccountStatus : Enumeration
    {
        public static readonly MerchantAccountStatus PENDING = new MerchantAccountStatus("pending");
        public static readonly MerchantAccountStatus ACTIVE = new MerchantAccountStatus("active");
        public static readonly MerchantAccountStatus SUSPENDED = new MerchantAccountStatus("suspended");

        public static readonly MerchantAccountStatus[] ALL = { PENDING, ACTIVE, SUSPENDED };

        protected MerchantAccountStatus(string name) : base(name) {}
    }

    public interface IMerchantAccount
    {
        string Id { get; }
        MerchantAccountStatus Status { get; }
        IMerchantAccount MasterMerchantAccount { get; }
        MerchantAccountIndividualDetails IndividualDetails { get; }
        IMerchantAccountBusinessDetails BusinessDetails { get; }
        IMerchantAccountFundingDetails FundingDetails { get; }
        bool IsSubMerchant { get; }
    }

    public class MerchantAccount : IMerchantAccount
    {
      public string Id { get; protected set; }
      public MerchantAccountStatus Status { get; protected set; }
      public IMerchantAccount MasterMerchantAccount { get; protected set; }
      public MerchantAccountIndividualDetails IndividualDetails { get; protected set; }
      public IMerchantAccountBusinessDetails BusinessDetails { get; protected set; }
      public IMerchantAccountFundingDetails FundingDetails { get; protected set; }

      public bool IsSubMerchant {
        get {
          return MasterMerchantAccount != null;
        }
      }

      protected internal MerchantAccount(NodeWrapper node)
      {
        Id = node.GetString("id");
        Status = (MerchantAccountStatus) CollectionUtil.Find(MerchantAccountStatus.ALL, node.GetString("status"), null);
        NodeWrapper masterNode = node.GetNode("master-merchant-account");
        if (masterNode != null)
            MasterMerchantAccount = new MerchantAccount(masterNode);
        else
            MasterMerchantAccount = null;
        NodeWrapper individualNode = node.GetNode("individual");
        if (individualNode != null)
            IndividualDetails = new MerchantAccountIndividualDetails(individualNode);
        else
            IndividualDetails = null;
        NodeWrapper businessNode = node.GetNode("business");
        if (businessNode != null)
            BusinessDetails = new MerchantAccountBusinessDetails(businessNode);
        else
            BusinessDetails = null;
        NodeWrapper fundingNode = node.GetNode("funding");
        if (fundingNode != null)
            FundingDetails = new MerchantAccountFundingDetails(fundingNode);
        else
            FundingDetails = null;
      }
    }
}
