﻿// Filename: UserGameCredentialRemovedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserGameCredentialRemovedIntegrationEventHandler : IIntegrationEventHandler<UserGameCredentialRemovedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserGameCredentialRemovedIntegrationEventHandler(IUserService userService, ILogger<UserGameCredentialRemovedIntegrationEventHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(UserGameCredentialRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.Credential.UserId);

            if (user != null)
            {
                var game = integrationEvent.Credential.Game.ToEnumeration<Game>();

                var claim = new Claim(CustomClaimTypes.GetGamePlayerFor(game), integrationEvent.Credential.PlayerId);

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
