// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Arena.Games.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Arena.Games.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserGamePlayerIdClaimAddedIntegrationEvent(this IServiceBusPublisher publisher, Credential credential)
        {
            await publisher.PublishAsync(new UserClaimsAddedIntegrationEvent(credential.UserId, new Claims(credential.ToClaim())));
        }

        public static async Task PublishUserGamePlayerIdClaimRemovedIntegrationEvent(this IServiceBusPublisher publisher, Credential credential)
        {
            await publisher.PublishAsync(new UserClaimsRemovedIntegrationEvent(credential.UserId, new Claims(credential.ToClaim())));
        }
    }
}
