// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Notifications.Application.Commands;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.ServiceBus;

using MediatR;

namespace eDoxa.Notifications.Application.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            await _mediator.SendCommandAsync(new CreateUserCommand(integrationEvent.UserId));
        }
    }
}