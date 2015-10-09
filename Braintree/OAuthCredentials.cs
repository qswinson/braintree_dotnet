using System;

namespace Braintree
{
    public interface IOAuthCredentials
    {
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
        string TokenType { get; set; }
        DateTime? ExpiresAt { get; set; }
    }

    public class OAuthCredentials : IOAuthCredentials
    {
        public OAuthCredentials(NodeWrapper node)
        {
            if (node == null) return;

            AccessToken = node.GetString("access-token");
            RefreshToken = node.GetString("refresh-token");
            TokenType = node.GetString("token-type");
            ExpiresAt = node.GetDateTime("expires-at");
        }

        public string AccessToken
        {
            get; set;
        }

        public string RefreshToken
        {
            get; set;
        }

        public string TokenType
        {
            get; set;
        }

        public DateTime? ExpiresAt
        {
            get; set;
        }
    }
}
