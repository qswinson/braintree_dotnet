using System;
using System.Collections.Generic;

namespace Braintree
{
    public interface IDiscountGateway
    {
        List<IDiscount> All();
    }
}