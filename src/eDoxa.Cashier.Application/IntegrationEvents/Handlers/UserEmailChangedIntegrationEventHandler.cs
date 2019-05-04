// Filename: UserEmailChangedIntegrationEventHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.ServiceBus;

using MediatR;

namespace eDoxa.Cashier.Application.IntegrationEvents.Handlers
{
    public class UserEmailChangedIntegrationEventHandler : IIntegrationEventHandler<UserEmailChangedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UserEmailChangedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserEmailChangedIntegrationEvent integrationEvent)
        {
            await _mediator.Send(new UpdateEmailCommand(integrationEvent.CustomerId, integrationEvent.Email));
        }
    }
}