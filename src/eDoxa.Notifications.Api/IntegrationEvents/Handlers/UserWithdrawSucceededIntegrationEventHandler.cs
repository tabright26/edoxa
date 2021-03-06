﻿// Filename: UserWithdrawSucceededIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Sendgrid;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Options;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserWithdrawSucceededIntegrationEventHandler : IIntegrationEventHandler<UserWithdrawSucceededIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IOptions<SendgridOptions> _options;

        public UserWithdrawSucceededIntegrationEventHandler(IUserService userService, IOptionsSnapshot<SendgridOptions> options)
        {
            _userService = userService;
            _options = options;
        }

        private SendgridOptions Options => _options.Value;

        public async Task HandleAsync(UserWithdrawSucceededIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(integrationEvent.UserId.ParseEntityId<UserId>(), Options.Templates.UserWithdrawSucceeded, integrationEvent);
        }
    }
}
