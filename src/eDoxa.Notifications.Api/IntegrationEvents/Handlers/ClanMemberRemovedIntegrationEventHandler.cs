// Filename: ClanMemberRemovedIntegrationEventHandler.cs
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
    public sealed class ClanMemberRemovedIntegrationEventHandler : IIntegrationEventHandler<ClanMemberRemovedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public ClanMemberRemovedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ClanMemberRemovedIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                nameof(ClanMemberRemovedIntegrationEvent),
                nameof(ClanMemberRemovedIntegrationEvent));
        }
    }
}
