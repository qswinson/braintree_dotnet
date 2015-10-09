using System;
using System.Collections.Generic;

namespace Braintree
{
    public interface ISettlementBatchSummary
    {
        IList<IDictionary<string, string>> Records { get; }
    }

    public class SettlementBatchSummary : ISettlementBatchSummary
    {
        private IList<IDictionary<string, string>> records;

        protected internal SettlementBatchSummary (NodeWrapper node)
        {
            records = new List<IDictionary<string, string>>();

            foreach (var record in node.GetList("records/record"))
            {
                records.Add(record.GetDictionary("."));
            }
        }

        public IList<IDictionary<string, string>> Records
        {
            get { return records; }
        }
    }
}

