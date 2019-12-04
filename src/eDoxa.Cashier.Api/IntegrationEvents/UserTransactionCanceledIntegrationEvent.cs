using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserTransactionCanceledIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserTransactionCanceledIntegrationEvent(UserId userId, TransactionId transactionId)
        {
            UserId = userId;
            TransactionId = transactionId;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserTransactionCanceled;
    }
}
