// Filename: ClanCandidatureSentIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Sendgrid;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Options;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class ClanCandidatureSentIntegrationEventHandler : IIntegrationEventHandler<ClanCandidatureSentIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IOptions<SendgridOptions> _options;

        public ClanCandidatureSentIntegrationEventHandler(IUserService userService, IOptionsSnapshot<SendgridOptions> options)
        {
            _userService = userService;
            _options = options;
        }

        private SendgridOptions Options => _options.Value;

        public async Task HandleAsync(ClanCandidatureSentIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(integrationEvent.Clan.OwnerId.ParseEntityId<UserId>(), Options.Templates.ClanCandidatureSent, integrationEvent);

            //$@"The user '{integrationEvent.UserId}' sent a member candidature to your clan '{integrationEvent.Clan.Name}'."
        }
    }
}
