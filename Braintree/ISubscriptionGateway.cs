#pragma warning disable 1591

using System;

namespace Braintree
{
    /// <summary>
    /// Provides operations for creating, finding, updating, searching, and deleting subscriptions in the vault
    /// </summary>
    public interface ISubscriptionGateway
    {
        Result<ISubscription> Cancel(string id);
        Result<Subscription> Create(SubscriptionRequest request);
        ISubscription Find(string id);
        Result<Transaction> RetryCharge(string subscriptionId);
        Result<Transaction> RetryCharge(string subscriptionId, decimal amount);
        ResourceCollection<ISubscription> Search(SubscriptionSearchRequest query);
        ResourceCollection<ISubscription> Search(SubscriptionGateway.SearchDelegate searchDelegate);
        Result<ISubscription> Update(string id, SubscriptionRequest request);
    }
}