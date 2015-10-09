using System;
using System.Collections.Generic;

namespace Braintree
{
    public class PlanDurationUnit : Enumeration
    {
        public static readonly PlanDurationUnit DAY = new PlanDurationUnit("day");
        public static readonly PlanDurationUnit MONTH = new PlanDurationUnit("month");
        public static readonly PlanDurationUnit UNRECOGNIZED = new PlanDurationUnit("unrecognized");
        public static readonly PlanDurationUnit[] ALL = { DAY, MONTH };
        protected PlanDurationUnit(string name) : base(name) {}
    }

    public interface IPlan
    {
        List<IAddOn> AddOns { get; }
        int? BillingDayOfMonth { get; }
        int? BillingFrequency { get; }
        string CurrencyIsoCode { get; }
        string Description { get; }
        List<IDiscount> Discounts { get; }
        string Id { get; }
        string Name { get; }
        int? NumberOfBillingCycles { get; }
        decimal? Price { get; }
        bool? TrialPeriod { get; }
        int? TrialDuration { get; }
        PlanDurationUnit TrialDurationUnit { get; }
    }

    public class Plan : IPlan
    {
        public List<IAddOn> AddOns { get; protected set; }
        public int? BillingDayOfMonth { get; protected set; }
        public int? BillingFrequency { get; protected set; }
        public string CurrencyIsoCode { get; protected set; }
        public string Description { get; protected set; }
        public List<IDiscount> Discounts { get; protected set; }
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public int? NumberOfBillingCycles { get; protected set; }
        public decimal? Price { get; protected set; }
        public bool? TrialPeriod { get; protected set; }
        public int? TrialDuration { get; protected set; }
        public PlanDurationUnit TrialDurationUnit { get; protected set; }

        public Plan(NodeWrapper node)
        {
            if (node == null) return;
            BillingDayOfMonth = node.GetInteger("billing-day-of-month");
            BillingFrequency = node.GetInteger("billing-frequency");
            CurrencyIsoCode = node.GetString("currency-iso-code");
            Description = node.GetString("description");
            Id = node.GetString("id");
            Name = node.GetString("name");
            NumberOfBillingCycles = node.GetInteger("number-of-billing-cycles");
            Price = node.GetDecimal("price");
            TrialPeriod = node.GetBoolean("trial-period");
            TrialDuration = node.GetInteger("trial-duration");
            string trialDurationUnitStr = node.GetString("trial-duration-unit");
            if (trialDurationUnitStr != null) {
                TrialDurationUnit = (PlanDurationUnit) CollectionUtil.Find(PlanDurationUnit.ALL, trialDurationUnitStr, PlanDurationUnit.UNRECOGNIZED);
            }
            AddOns = new List<IAddOn> ();
            foreach (var addOnResponse in node.GetList("add-ons/add-on")) {
                AddOns.Add(new AddOn(addOnResponse));
            }
            Discounts = new List<IDiscount> ();
            foreach (var discountResponse in node.GetList("discounts/discount")) {
                Discounts.Add(new Discount(discountResponse));
            }
        }
    }
}
