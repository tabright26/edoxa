// Filename: UserWithdrawalSucceededIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Notifications.Api.Application;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserWithdrawalSucceededIntegrationEventHandler : IIntegrationEventHandler<UserWithdrawalSucceededIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserWithdrawalSucceededIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserWithdrawalSucceededIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                SendGridTemplates.UserWithdrawalSucceeded,
                integrationEvent);
        }
    }
}
