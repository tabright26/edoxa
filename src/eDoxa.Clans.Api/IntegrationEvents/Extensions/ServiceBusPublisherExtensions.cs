// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Grpc.Protos.Clans.Dtos;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Clans.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishClanMemberAddedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, Clan clan)
        {
            var integrationEvent = new ClanMemberAddedIntegrationEvent
            {
                UserId = userId,
                Clan = new ClanDto
                {
                    Id = clan.Id,
                    Name = clan.Name,
                    Summary = clan.Summary,
                    OwnerId = clan.OwnerId
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishClanMemberRemovedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, Clan clan)
        {
            var integrationEvent = new ClanMemberRemovedIntegrationEvent
            {
                UserId = userId,
                Clan = new ClanDto
                {
                    Id = clan.Id,
                    Name = clan.Name,
                    Summary = clan.Summary,
                    OwnerId = clan.OwnerId
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishClanCandidatureSentIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, Clan clan)
        {
            var integrationEvent = new ClanCandidatureSentIntegrationEvent
            {
                UserId = userId,
                Clan = new ClanDto
                {
                    Id = clan.Id,
                    Name = clan.Name,
                    Summary = clan.Summary,
                    OwnerId = clan.OwnerId
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishClanInvitationSentIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, Clan clan)
        {
            var integrationEvent = new ClanInvitationSentIntegrationEvent
            {
                UserId = userId,
                Clan = new ClanDto
                {
                    Id = clan.Id,
                    Name = clan.Name,
                    Summary = clan.Summary,
                    OwnerId = clan.OwnerId
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
