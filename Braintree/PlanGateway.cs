using System;
using System.Collections.Generic;

namespace Braintree
{
    public class PlanGateway : IPlanGateway
    {
        private readonly BraintreeService service;

        public PlanGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            service = new BraintreeService(gateway.Configuration);
        }

        public virtual List<IPlan> All()
        {
            var response = new NodeWrapper(service.Get(service.MerchantPath() + "/plans"));

            var plans = new List<IPlan>();
            foreach (var node in response.GetList("plan"))
            {
                plans.Add(new Plan(node));
            }
            return plans;
        }
    }
}

