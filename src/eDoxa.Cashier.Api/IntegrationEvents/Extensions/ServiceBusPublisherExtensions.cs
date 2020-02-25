// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishChallengeClosedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            ChallengeId challengeId,
            ChallengeParticipantPayouts payouts
        )
        {
            var integrationEvent = new ChallengeClosedIntegrationEvent
            {
                ChallengeId = challengeId,
                PayoutPrizes =
                {
                    payouts.ToDictionary(
                        payoutPrize => payoutPrize.Key.ToString(),
                        payoutPrize => new CurrencyDto
                        {
                            Amount = payoutPrize.Value.Amount,
                            Type = payoutPrize.Value.Type.ToEnum<EnumCurrencyType>()
                        })
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserDepositSucceededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionDto transaction
        )
        {
            var integrationEvent = new UserDepositSucceededIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserDepositFailedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, TransactionDto transaction)
        {
            var integrationEvent = new UserDepositFailedIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserDepositCanceledIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, TransactionDto transaction)
        {
            var integrationEvent = new UserDepositCanceledIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
