using System;

namespace Braintree
{
    public interface IMerchant
    {
        string Id { get; set; }
        string Email { get; set; }
        string CompanyName { get; set; }
        string CountryCodeAlpha3 { get; set; }
        string CountryCodeAlpha2 { get; set; }
        string CountryCodeNumeric { get; set; }
        string CountryName { get; set; }
    }

    public class Merchant : IMerchant
    {
        public Merchant(NodeWrapper node)
        {
            if (node == null) return;

            NodeWrapper merchantNode = node.GetNode("merchant");

            Id = merchantNode.GetString("id");
            Email = merchantNode.GetString("email");
            CompanyName = merchantNode.GetString("company-name");
            CountryCodeAlpha3 = merchantNode.GetString("country-code-alpha3");
            CountryCodeAlpha2 = merchantNode.GetString("country-code-alpha2");
            CountryCodeNumeric = merchantNode.GetString("country-code-numeric");
            CountryName = merchantNode.GetString("country-name");

            Credentials = new OAuthCredentials(node.GetNode("credentials"));
        }

        public OAuthCredentials Credentials;

        public string Id
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string CompanyName
        {
            get; set;
        }

        public string CountryCodeAlpha3
        {
            get; set;
        }

        public string CountryCodeAlpha2
        {
            get; set;
        }

        public string CountryCodeNumeric
        {
            get; set;
        }

        public string CountryName
        {
            get; set;
        }
    }
}
