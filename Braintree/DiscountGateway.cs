using System;
using System.Collections.Generic;
using System.Xml;

namespace Braintree
{
    public class DiscountGateway : IDiscountGateway
    {
        private readonly BraintreeService service;

        public DiscountGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            service = new BraintreeService(gateway.Configuration);
        }

        public virtual List<IDiscount> All()
        {
            var response = new NodeWrapper(service.Get(service.MerchantPath() + "/discounts"));

            var discounts = new List<IDiscount>();
            foreach (var node in response.GetList("discount"))
            {
                discounts.Add(new Discount(node));
            }
            return discounts;
        }
    }
}

