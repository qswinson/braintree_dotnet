using System;
using System.Collections.Generic;

namespace Braintree
{
    public interface IThreeDSecureInfo
    {
        string Status { get; }
        string Enrolled { get; }
        bool? LiabilityShifted { get; }
        bool? LiabilityShiftPossible { get; }
    }

    public class ThreeDSecureInfo : IThreeDSecureInfo
    {
        public string Status { get; protected set; }
        public string Enrolled { get; protected set; }
        public bool? LiabilityShifted { get; protected set; }
        public bool? LiabilityShiftPossible { get; protected set; }

        public ThreeDSecureInfo(NodeWrapper node)
        {
            if (node == null) return;

            Enrolled = node.GetString("enrolled");
            Status = node.GetString("status");
            LiabilityShifted = node.GetBoolean("liability-shifted");
            LiabilityShiftPossible = node.GetBoolean("liability-shift-possible");
        }
    }
}

