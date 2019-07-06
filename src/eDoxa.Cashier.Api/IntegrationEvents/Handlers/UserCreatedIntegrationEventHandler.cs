// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.IntegrationEvents;

using MediatR;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            await _mediator.Send(new CreateUserCommand(UserId.FromGuid(integrationEvent.UserId)));
        }
    }
}
