#pragma warning disable 1591

using System;

namespace Braintree
{
    public interface IWebhookNotificationGateway
    {
        IWebhookNotification Parse(string signature, string payload);
        string Verify(string challenge);
    }
}