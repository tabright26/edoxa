// Filename: ClanCandidatureSentIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class ClanCandidatureSentIntegrationEventHandler : IIntegrationEventHandler<ClanCandidatureSentIntegrationEvent>
    {
        private readonly IUserService _userService;

        public ClanCandidatureSentIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ClanCandidatureSentIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.Clan.OwnerId.ParseEntityId<UserId>(),
                "eDoxa - Clan candidature",
                $@"The user '{integrationEvent.UserId}' sent a member candidature to your clan '{integrationEvent.Clan.Name}'.");
        }
    }
}
