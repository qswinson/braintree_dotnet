#pragma warning disable 1591

using System;

namespace Braintree
{
    public interface IMerchantAccountIndividualDetails
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Phone { get; }
        string DateOfBirth { get; }
        string SsnLastFour { get; }
        IAddress Address { get; }
    }

    public class MerchantAccountIndividualDetails : IMerchantAccountIndividualDetails
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Phone { get; protected set; }
        public string DateOfBirth { get; protected set; }
        public string SsnLastFour { get; protected set; }
        public IAddress Address { get; protected set; }

        protected internal MerchantAccountIndividualDetails(NodeWrapper node)
        {
            FirstName = node.GetString("first-name");
            LastName = node.GetString("last-name");
            Email = node.GetString("email");
            Phone = node.GetString("phone");
            DateOfBirth = node.GetString("date-of-birth");
            SsnLastFour = node.GetString("ssn-last-4");
            Address = new Address(node.GetNode("address"));
        }
    }
}
