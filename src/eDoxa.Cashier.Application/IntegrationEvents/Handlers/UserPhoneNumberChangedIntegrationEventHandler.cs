// Filename: UserPhoneNumberChangedIntegrationEventHandler.cs
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
    public class UserPhoneNumberChangedIntegrationEventHandler : IIntegrationEventHandler<UserPhoneNumberChangedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UserPhoneNumberChangedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserPhoneNumberChangedIntegrationEvent integrationEvent)
        {
            await _mediator.Send(new UpdatePhoneCommand(integrationEvent.CustomerId, integrationEvent.PhoneNumber));
        }
    }
}