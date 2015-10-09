using System.Collections.Generic;

namespace Braintree
{
    public interface IAddOnGateway
    {
        List<IAddOn> All();
    }
}