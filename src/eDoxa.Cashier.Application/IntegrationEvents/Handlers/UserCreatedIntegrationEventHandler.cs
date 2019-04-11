﻿// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.ServiceBus;

using MediatR;

namespace eDoxa.Cashier.Application.IntegrationEvents.Handlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            await _mediator.Send(new CreateUserCommand(integrationEvent.UserId, integrationEvent.Email));
        }
    }
}