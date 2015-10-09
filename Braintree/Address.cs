#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Braintree
{
    public interface IAddress
    {
        string Id { get; }
        string CustomerId { get; }
        string FirstName { get; }
        string LastName { get; }
        string Company { get; }
        string StreetAddress { get; }
        string ExtendedAddress { get; }
        string Locality { get; }
        string Region { get; }
        string PostalCode { get; }
        string CountryCodeAlpha2 { get; }
        string CountryCodeAlpha3 { get; }
        string CountryCodeNumeric { get; }
        string CountryName { get; }
        DateTime? CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }

    /// <summary>
    /// An address returned by the Braintree Gateway
    /// </summary>
    /// <remarks>
    /// An address can belong to:
    /// <ul>
    ///   <li>a <see cref="CreditCard"/> as the billing address</li>
    ///   <li>a <see cref="Customer"/> as an address</li>
    ///   <li>a <see cref="Transaction"/> as a billing or shipping address</li>
    /// </ul>
    /// </remarks>
    /// <example>
    /// Addresses can be retrieved via the gateway using the associated customer Id and address Id:
    /// <code>
    ///     Address address = gateway.Address.Find("customerId", "addressId");
    /// </code>
    /// </example>
    public class Address : IAddress
    {
        public string Id { get; protected set; }
        public string CustomerId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Company { get; protected set; }
        public string StreetAddress { get; protected set; }
        public string ExtendedAddress { get; protected set; }
        public string Locality { get; protected set; }
        public string Region { get; protected set; }
        public string PostalCode { get; protected set; }
        public string CountryCodeAlpha2 { get; protected set; }
        public string CountryCodeAlpha3 { get; protected set; }
        public string CountryCodeNumeric { get; protected set; }
        public string CountryName { get; protected set; }
        public DateTime? CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected internal Address(NodeWrapper node)
        {
            if (node == null) return;

            Id = node.GetString("id");
            CustomerId = node.GetString("customer-id");
            FirstName = node.GetString("first-name");
            LastName = node.GetString("last-name");
            Company = node.GetString("company");
            StreetAddress = node.GetString("street-address");
            ExtendedAddress = node.GetString("extended-address");
            Locality = node.GetString("locality");
            Region = node.GetString("region");
            PostalCode = node.GetString("postal-code");
            CountryCodeAlpha2 = node.GetString("country-code-alpha2");
            CountryCodeAlpha3 = node.GetString("country-code-alpha3");
            CountryCodeNumeric = node.GetString("country-code-numeric");
            CountryName = node.GetString("country-name");
            CreatedAt = node.GetDateTime("created-at");
            UpdatedAt = node.GetDateTime("updated-at");
        }
    }
}
