// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
            PayoutPrizes payoutPrizes
        )
        {
            var integrationEvent = new ChallengeClosedIntegrationEvent
            {
                ChallengeId = challengeId,
                PayoutPrizes =
                {
                    payoutPrizes.ToDictionary(
                        payoutPrize => payoutPrize.Key.ToString(),
                        payoutPrize => new PrizeDto
                        {
                            Amount = payoutPrize.Value.Amount,
                            Currency = payoutPrize.Value.Currency.ToEnum<EnumCurrency>()
                        })
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishCreateChallengePayoutFailedIntegrationEventAsync(this IServiceBusPublisher publisher, ChallengeId challengeId)
        {
            var integrationEvent = new CreateChallengePayoutFailedIntegrationEvent
            {
                ChallengeId = challengeId
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
