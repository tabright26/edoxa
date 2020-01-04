// Filename: ClanInvitationSentIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Notifications.Api.Application;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class ClanInvitationSentIntegrationEventHandler : IIntegrationEventHandler<ClanInvitationSentIntegrationEvent>
    {
        private readonly IUserService _userService;

        public ClanInvitationSentIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ClanInvitationSentIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                SendGridTemplates.ClanInvitationSent,
                integrationEvent);

            //$@"The clan '{integrationEvent.Clan.Name}' sent you an invitation to became a member.";
        }
    }
}
