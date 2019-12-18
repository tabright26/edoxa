﻿// Filename: UserGameCredentialRemovedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserGameCredentialRemovedIntegrationEventHandler : IIntegrationEventHandler<UserGameCredentialRemovedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserGameCredentialRemovedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserGameCredentialRemovedIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.Credential.UserId.ParseEntityId<UserId>(),
                nameof(UserGameCredentialRemovedIntegrationEvent),
                nameof(UserGameCredentialRemovedIntegrationEvent));
        }
    }
}
