﻿// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Clans.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserClaimClanIdAddedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, ClanId clanId)
        {
            await publisher.PublishAsync(new UserClaimsAddedIntegrationEvent(userId, new Claims(new Claim(ClaimTypes.ClanId, clanId.ToString()))));
        }

        public static async Task PublishUserClaimClanIdRemovedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, ClanId clanId)
        {
            await publisher.PublishAsync(new UserClaimsRemovedIntegrationEvent(userId, new Claims(new Claim(ClaimTypes.ClanId, clanId.ToString()))));
        }

        public static async Task PublishUserEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string subject,
            string htmlMessage
        )
        {
            await publisher.PublishAsync(new UserEmailSentIntegrationEvent(userId, subject, htmlMessage));
        }
    }
}