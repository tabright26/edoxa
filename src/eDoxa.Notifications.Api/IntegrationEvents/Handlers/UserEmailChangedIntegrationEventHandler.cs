﻿// Filename: UserEmailChangedIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Sendgrid;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserEmailChangedIntegrationEventHandler : IIntegrationEventHandler<UserEmailChangedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IOptions<SendgridOptions> _options;

        public UserEmailChangedIntegrationEventHandler(
            IUserService userService,
            ILogger<UserEmailChangedIntegrationEventHandler> logger,
            IOptionsSnapshot<SendgridOptions> options
        )
        {
            _userService = userService;
            _logger = logger;
            _options = options;
        }

        private SendgridOptions Options => _options.Value;

        public async Task HandleAsync(UserEmailChangedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (await _userService.UserExistsAsync(userId))
            {
                var user = await _userService.FindUserAsync(userId);

                var result = await _userService.UpdateUserAsync(user, integrationEvent.Email.Address);

                if (result.IsValid)
                {
                    await _userService.SendEmailAsync(integrationEvent.UserId.ParseEntityId<UserId>(), Options.Templates.UserEmailChanged, integrationEvent);
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
