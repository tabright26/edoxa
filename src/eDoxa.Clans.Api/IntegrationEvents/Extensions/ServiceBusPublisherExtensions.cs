// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Clans.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserClaimClanIdAddedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, ClanId clanId)
        {
            await publisher.PublishAsync(
                new UserClaimsAddedIntegrationEvent
                {
                    UserId = userId,
                    Claims =
                    {
                        new UserClaimDto
                        {
                            Type = CustomClaimTypes.Clan,
                            Value = clanId
                        }
                    }
                });
        }

        public static async Task PublishUserClaimClanIdRemovedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, ClanId clanId)
        {
            await publisher.PublishAsync(
                new UserClaimsRemovedIntegrationEvent
                {
                    UserId = userId,
                    Claims =
                    {
                        new UserClaimDto
                        {
                            Type = CustomClaimTypes.Clan,
                            Value = clanId
                        }
                    }
                });
        }

        public static async Task PublishUserEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string subject,
            string htmlMessage
        )
        {
            await publisher.PublishAsync(
                new UserEmailSentIntegrationEvent
                {
                    UserId = userId,
                    Subject = subject,
                    HtmlMessage = htmlMessage
                });
        }
    }
}
