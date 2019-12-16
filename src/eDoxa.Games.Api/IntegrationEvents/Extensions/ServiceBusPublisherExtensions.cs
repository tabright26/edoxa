// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Games.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserGamePlayerIdClaimAddedIntegrationEvent(this IServiceBusPublisher publisher, Credential credential)
        {
            await publisher.PublishAsync(
                new UserClaimsAddedIntegrationEvent
                {
                    UserId = credential.UserId,
                    Claims =
                    {
                        new UserClaimDto
                        {
                            Type = CustomClaimTypes.GetGamePlayerFor(credential.Game),
                            Value = credential.PlayerId
                        }
                    }
                });
        }

        public static async Task PublishUserGamePlayerIdClaimRemovedIntegrationEvent(this IServiceBusPublisher publisher, Credential credential)
        {
            await publisher.PublishAsync(
                new UserClaimsRemovedIntegrationEvent
                {
                    UserId = credential.UserId,
                    Claims =
                    {
                        new UserClaimDto
                        {
                            Type = CustomClaimTypes.GetGamePlayerFor(credential.Game),
                            Value = credential.PlayerId
                        }
                    }
                });
        }
    }
}
