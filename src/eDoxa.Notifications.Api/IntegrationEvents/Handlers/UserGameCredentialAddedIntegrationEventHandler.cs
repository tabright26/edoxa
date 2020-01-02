// Filename: UserGameCredentialAddedIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Notifications.Api.Application;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserGameCredentialAddedIntegrationEventHandler : IIntegrationEventHandler<UserGameCredentialAddedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserGameCredentialAddedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserGameCredentialAddedIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.Credential.UserId.ParseEntityId<UserId>(),
                SendGridTemplates.UserGameCredentialAdded,
                integrationEvent);
        }
    }
}
