#pragma warning disable 1591

using System;

namespace Braintree
{
    public interface IDisbursementDetails
    {
        decimal? SettlementAmount { get; }
        string SettlementCurrencyIsoCode { get; }
        string SettlementCurrencyExchangeRate { get; }
        bool? FundsHeld { get; }
        bool? Success { get; }
        DateTime? DisbursementDate { get; }
        bool IsValid();
    }

    public class DisbursementDetails : IDisbursementDetails
    {
        public decimal? SettlementAmount { get; protected set; }
        public string SettlementCurrencyIsoCode { get; protected set; }
        public string SettlementCurrencyExchangeRate { get; protected set; }
        public bool? FundsHeld { get; protected set; }
        public bool? Success { get; protected set; }
        public DateTime? DisbursementDate { get; protected set; }

        protected internal DisbursementDetails(NodeWrapper node)
        {
            SettlementAmount = node.GetDecimal("settlement-amount");
            SettlementCurrencyIsoCode = node.GetString("settlement-currency-iso-code");
            SettlementCurrencyExchangeRate = node.GetString("settlement-currency-exchange-rate");
            FundsHeld = node.GetBoolean("funds-held");
            Success = node.GetBoolean("success");
            DisbursementDate = node.GetDateTime("disbursement-date");
        }

        public bool IsValid()
        {
            return DisbursementDate != null;
        }
    }
}
