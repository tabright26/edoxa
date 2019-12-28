﻿// Filename: UserWithdrawalFailedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserWithdrawalFailedIntegrationEventHandler : IIntegrationEventHandler<UserWithdrawalFailedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserWithdrawalFailedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserWithdrawalFailedIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                nameof(UserWithdrawalFailedIntegrationEvent),
                nameof(UserWithdrawalFailedIntegrationEvent));
        }
    }
}