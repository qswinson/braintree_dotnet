#pragma warning disable 1591

namespace Braintree
{
    public interface IAddOn : IModification
    {
    }

    public class AddOn : Modification, IAddOn
    {
        protected internal AddOn(NodeWrapper node) : base(node) {
        }
    }
}
