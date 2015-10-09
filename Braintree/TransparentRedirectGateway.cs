#pragma warning disable 1591

using System;
using System.Xml;

namespace Braintree
{
    public class TransparentRedirectGateway : ITransparentRedirectGateway
    {
        public const string CREATE_CUSTOMER = "create_customer";
        public const string UPDATE_CUSTOMER = "update_customer";
        public const string CREATE_PAYMENT_METHOD = "create_payment_method";
        public const string UPDATE_PAYMENT_METHOD = "update_payment_method";
        public const string CREATE_TRANSACTION = "create_transaction";

        public string Url
        {
            get { return service.BaseMerchantURL() + "/transparent_redirect_requests"; }
        }

        private readonly BraintreeService service;
        private readonly BraintreeGateway gateway;

        protected internal TransparentRedirectGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            this.gateway = gateway;
            service = new BraintreeService(gateway.Configuration);
        }

        public string BuildTrData(Request request, string redirectURL)
        {
            return TrUtil.BuildTrData(request, redirectURL, service);
        }

        public virtual Result<ITransaction> ConfirmTransaction(string queryString)
        {
            var trRequest = new TransparentRedirectRequest(queryString, service);
            XmlNode node = service.Post(service.MerchantPath() + "/transparent_redirect_requests/" + trRequest.Id + "/confirm", trRequest);

            return new ResultImpl<ITransaction>(new NodeWrapper(node), gateway);
        }

        public virtual Result<ICustomer> ConfirmCustomer(string queryString)
        {
            var trRequest = new TransparentRedirectRequest(queryString, service);
            XmlNode node = service.Post(service.MerchantPath() + "/transparent_redirect_requests/" + trRequest.Id + "/confirm", trRequest);

            return new ResultImpl<ICustomer>(new NodeWrapper(node), gateway);
        }

        public virtual Result<ICreditCard> ConfirmCreditCard(string queryString)
        {
            var trRequest = new TransparentRedirectRequest(queryString, service);
            XmlNode node = service.Post(service.MerchantPath() + "/transparent_redirect_requests/" + trRequest.Id + "/confirm", trRequest);

            return new ResultImpl<ICreditCard>(new NodeWrapper(node), gateway);
        }
    }
}
