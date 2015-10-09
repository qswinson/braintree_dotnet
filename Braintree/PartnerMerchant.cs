using System;

namespace Braintree
{
    public interface IPartnerMerchant
    {
        string MerchantPublicId { get; }
        string PublicKey { get; }
        string PrivateKey { get; }
        string PartnerMerchantId { get; }
        string ClientSideEncryptionKey { get; }
    }

    public class PartnerMerchant : IPartnerMerchant
    {
        public string MerchantPublicId { get; protected set; }
        public string PublicKey { get; protected set; }
        public string PrivateKey { get; protected set; }
        public string PartnerMerchantId { get; protected set; }
        public string ClientSideEncryptionKey { get; protected set; }

        protected internal PartnerMerchant(NodeWrapper node)
        {
            MerchantPublicId = node.GetString("merchant-public-id");
            PublicKey = node.GetString("public-key");
            PrivateKey = node.GetString("private-key");
            PartnerMerchantId = node.GetString("partner-merchant-id");
            ClientSideEncryptionKey = node.GetString("client-side-encryption-key");
        }
    }
}

