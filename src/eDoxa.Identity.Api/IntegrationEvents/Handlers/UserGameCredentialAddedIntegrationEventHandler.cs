// Filename: UserGameCredentialAddedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserGameCredentialAddedIntegrationEventHandler : IIntegrationEventHandler<UserGameCredentialAddedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserGameCredentialAddedIntegrationEventHandler(IUserService userService, ILogger<UserGameCredentialAddedIntegrationEventHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(UserGameCredentialAddedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.Credential.UserId);

            if (user != null)
            {
                var game = integrationEvent.Credential.Game.ToEnumeration<Game>();

                var claim = new Claim(CustomClaimTypes.GetGamePlayerFor(game), integrationEvent.Credential.PlayerId);

                var result = await _userService.AddClaimAsync(user, claim);

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
