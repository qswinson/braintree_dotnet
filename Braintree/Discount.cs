#pragma warning disable 1591

namespace Braintree
{
    public interface IDiscount : IModification { }

    public class Discount : Modification, IDiscount
    {
        protected internal Discount(NodeWrapper node) : base(node) {
        }
    }
}
