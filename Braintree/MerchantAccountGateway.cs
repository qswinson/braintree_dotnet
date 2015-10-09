using System;
using System.Collections.Generic;
using System.Xml;
using Braintree.Exceptions;

namespace Braintree
{
    public class MerchantAccountGateway : IMerchantAccountGateway
    {
        private readonly BraintreeService service;
        private readonly BraintreeGateway gateway;

        protected internal MerchantAccountGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            this.gateway = gateway;
            service = new BraintreeService(gateway.Configuration);
        }

        public virtual Result<IMerchantAccount> Create(MerchantAccountRequest request)
        {
            XmlNode merchantAccountXML = service.Post(service.MerchantPath() + "/merchant_accounts/create_via_api", request);

            return new ResultImpl<IMerchantAccount>(new NodeWrapper(merchantAccountXML), gateway);
        }

        public virtual Result<IMerchantAccount> Update(string id, MerchantAccountRequest request)
        {
            XmlNode merchantAccountXML = service.Put(service.MerchantPath() + "/merchant_accounts/" + id + "/update_via_api", request);

            return new ResultImpl<IMerchantAccount>(new NodeWrapper(merchantAccountXML), gateway);
        }

        public virtual IMerchantAccount Find(string id)
        {
            if(id == null || id.Trim().Equals(""))
                throw new NotFoundException();

            XmlNode merchantAccountXML = service.Get(service.MerchantPath() + "/merchant_accounts/" + id);

            return new MerchantAccount(new NodeWrapper(merchantAccountXML));
        }
    }
}
