#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Xml;
using Braintree.Exceptions;

namespace Braintree
{
    /// <summary>
    /// Provides operations for sales, credits, refunds, voids, submitting for settlement, and searching for transactions in the vault
    /// </summary>
    public class TransactionGateway : ITransactionGateway
    {
        private readonly BraintreeService service;
        private readonly BraintreeGateway gateway;

        protected internal TransactionGateway(BraintreeGateway gateway)
        {
            gateway.Configuration.AssertHasAccessTokenOrKeys();
            this.gateway = gateway;
            service = new BraintreeService(gateway.Configuration);
        }

        [Obsolete("Use gateway.TransparentRedirect.Url")]
        public virtual string TransparentRedirectURLForCreate()
        {
            return service.BaseMerchantURL() + "/transactions/all/create_via_transparent_redirect_request";
        }

        public virtual Result<ITransaction> CancelRelease(string id)
        {
            var request = new TransactionRequest();

            XmlNode response = service.Put(service.MerchantPath() + "/transactions/" + id + "/cancel_release", request);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        [Obsolete("Use gateway.TransparentRedirect.Confirm()")]
        public virtual Result<ITransaction> ConfirmTransparentRedirect(string queryString)
        {
            var trRequest = new TransparentRedirectRequest(queryString, service);
            XmlNode node = service.Post(service.MerchantPath() + "/transactions/all/confirm_transparent_redirect_request", trRequest);

            return new ResultImpl<ITransaction>(new NodeWrapper(node), gateway);
        }

        public virtual Result<ITransaction> HoldInEscrow(string id)
        {
            var request = new TransactionRequest();

            XmlNode response = service.Put(service.MerchantPath() + "/transactions/" + id + "/hold_in_escrow", request);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual string SaleTrData(TransactionRequest trData, string redirectURL)
        {
            trData.Type = TransactionType.SALE;

            return TrUtil.BuildTrData(trData, redirectURL, service);
        }

        public virtual string CreditTrData(TransactionRequest trData, string redirectURL)
        {
            trData.Type = TransactionType.CREDIT;

            return TrUtil.BuildTrData(trData, redirectURL, service);
        }

        public virtual Result<ITransaction> Credit(TransactionRequest request)
        {
            request.Type = TransactionType.CREDIT;
            XmlNode response = service.Post(service.MerchantPath() + "/transactions", request);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual Transaction Find(string id)
        {
            if(id == null || id.Trim().Equals(""))
                throw new NotFoundException();

            XmlNode response = service.Get(service.MerchantPath() + "/transactions/" + id);

            return new Transaction(new NodeWrapper(response), gateway);
        }

        public virtual Result<Transaction> Refund(string id)
        {
            XmlNode response = service.Post(service.MerchantPath() + "/transactions/" + id + "/refund");
            return new ResultImpl<Transaction>(new NodeWrapper(response), gateway);
        }

        public virtual Result<ITransaction> Refund(string id, decimal amount)
        {
            var request = new TransactionRequest
            {
                Amount = amount
            };
            XmlNode response = service.Post(service.MerchantPath() + "/transactions/" + id + "/refund", request);
            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual Result<ITransaction> Sale(TransactionRequest request)
        {
            request.Type = TransactionType.SALE;
            XmlNode response = service.Post(service.MerchantPath() + "/transactions", request);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual Result<ITransaction> ReleaseFromEscrow(string id)
        {
            var request = new TransactionRequest();

            XmlNode response = service.Put(service.MerchantPath() + "/transactions/" + id + "/release_from_escrow", request);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual Result<ITransaction> SubmitForSettlement(string id)
        {
            return SubmitForSettlement(id, 0);
        }

        public virtual Result<ITransaction> SubmitForSettlement(string id, decimal amount)
        {
            var request = new TransactionRequest();
            request.Amount = amount;

            XmlNode response = service.Put(service.MerchantPath() + "/transactions/" + id + "/submit_for_settlement", request);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual Result<Transaction> SubmitForPartialSettlement(string id, decimal amount)
        {
            var request = new TransactionRequest();
            request.Amount = amount;

            XmlNode response = service.Post(service.MerchantPath() + "/transactions/" + id + "/submit_for_partial_settlement", request);

            return new ResultImpl<Transaction>(new NodeWrapper(response), gateway);
        }

        public virtual Result<ITransaction> Void(string id)
        {
            XmlNode response = service.Put(service.MerchantPath() + "/transactions/" + id + "/void");

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        public virtual ResourceCollection<ITransaction> Search(TransactionSearchRequest query)
        {
            var response = new NodeWrapper(service.Post(service.MerchantPath() + "/transactions/advanced_search_ids", query));

            if (response.GetName() == "search-results") {
                return new ResourceCollection<ITransaction>(response, delegate(string[] ids) {
                    return FetchTransactions(query, ids);
                });
            } else {
                throw new DownForMaintenanceException();
            }
        }

        public virtual Result<ITransaction> CloneTransaction(string id, TransactionCloneRequest cloneRequest)
        {
            XmlNode response = service.Post(service.MerchantPath() + "/transactions/" + id + "/clone", cloneRequest);

            return new ResultImpl<ITransaction>(new NodeWrapper(response), gateway);
        }

        private List<ITransaction> FetchTransactions(TransactionSearchRequest query, string[] ids)
        {
            query.Ids.IncludedIn(ids);

            var response = new NodeWrapper(service.Post(service.MerchantPath() + "/transactions/advanced_search", query));

            var transactions = new List<ITransaction>();
            foreach (var node in response.GetList("transaction"))
            {
                transactions.Add(new Transaction(node, gateway));
            }
            return transactions;
        }
    }
}
