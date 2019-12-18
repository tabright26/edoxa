// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Games.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserGameCredentialAddedIntegrationEventAsync(this IServiceBusPublisher publisher, Credential credential)
        {
            var integrationEvent = new UserGameCredentialAddedIntegrationEvent
            {
                Credential = new GameCredentialDto
                {
                    UserId = credential.UserId,
                    PlayerId = credential.PlayerId,
                    Game = credential.Game.ToEnum<GameDto>()
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserGameCredentialRemovedIntegrationEventAsync(this IServiceBusPublisher publisher, Credential credential)
        {
            var integrationEvent = new UserGameCredentialRemovedIntegrationEvent
            {
                Credential = new GameCredentialDto
                {
                    UserId = credential.UserId,
                    PlayerId = credential.PlayerId,
                    Game = credential.Game.ToEnum<GameDto>()
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
