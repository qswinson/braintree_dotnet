using System;
using System.Collections.Generic;

namespace Braintree
{
    public interface IRiskData
    {
        string id { get; }
        string decision { get; }
    }

    public class RiskData : IRiskData
    {
        public string id { get; protected set; }
        public string decision { get; protected set; }

        public RiskData(NodeWrapper node)
        {
            if (node == null) return;
            id = node.GetString("id");
            decision = node.GetString("decision");
        }
    }
}

