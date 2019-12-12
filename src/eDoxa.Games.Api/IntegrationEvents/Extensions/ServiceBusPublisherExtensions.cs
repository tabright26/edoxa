// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Application.Extensions;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Games.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserGamePlayerIdClaimAddedIntegrationEvent(this IServiceBusPublisher publisher, Credential credential)
        {
            var claim = credential.ToClaim();

            await publisher.PublishAsync(
                new UserClaimsAddedIntegrationEvent
                {
                    UserId = credential.UserId,
                    Claims =
                    {
                        new UserClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    }
                });
        }

        public static async Task PublishUserGamePlayerIdClaimRemovedIntegrationEvent(this IServiceBusPublisher publisher, Credential credential)
        {
            var claim = credential.ToClaim();

            await publisher.PublishAsync(
                new UserClaimsRemovedIntegrationEvent
                {
                    UserId = credential.UserId,
                    Claims =
                    {
                        new UserClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    }
                });
        }
    }
}
