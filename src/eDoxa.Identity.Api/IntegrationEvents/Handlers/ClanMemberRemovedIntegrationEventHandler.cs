// Filename: ClanMemberRemovedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class ClanMemberRemovedIntegrationEventHandler : IIntegrationEventHandler<ClanMemberRemovedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public ClanMemberRemovedIntegrationEventHandler(IUserService userService, ILogger<ClanMemberRemovedIntegrationEventHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(ClanMemberRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId);

            if (user != null)
            {
                var claim = new Claim(CustomClaimTypes.Clan, integrationEvent.Clan.Id);

                var result = await _userService.RemoveClaimAsync(user, claim);

                if (result.Succeeded)
                {
                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogError(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogCritical(""); // FRANCIS: TODO.
            }
        }
    }
}
