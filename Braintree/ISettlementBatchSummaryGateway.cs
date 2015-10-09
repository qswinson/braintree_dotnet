using System;

namespace Braintree
{
    public interface ISettlementBatchSummaryGateway
    {
        Result<ISettlementBatchSummary> Generate(DateTime settlementDate);
        Result<ISettlementBatchSummary> Generate(DateTime settlementDate, string groupByCustomField);
    }
}