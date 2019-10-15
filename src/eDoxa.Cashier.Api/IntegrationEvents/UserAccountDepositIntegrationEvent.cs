// Filename: UserAccountDepositIntegrationEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserAccountDepositIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserAccountDepositIntegrationEvent(
            UserId userId,
            string email,
            TransactionId transactionId,
            string description,
            long amount
        )
        {
            UserId = userId;
            Email = email;
            TransactionId = transactionId;
            Description = description;
            Amount = amount;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string Email { get; }

        [JsonProperty]
        public TransactionId TransactionId { get; }

        [JsonProperty]
        public string Description { get; }

        [JsonProperty]
        public long Amount { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserAccountDeposit;
    }
}
