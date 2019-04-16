// Filename: UserNotifiedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Notifications.Application.Commands;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.ServiceBus;

using MediatR;

namespace eDoxa.Notifications.Application.IntegrationEvents.Handlers
{
    public class UserNotifiedIntegrationEventHandler : IIntegrationEventHandler<UserNotifiedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UserNotifiedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserNotifiedIntegrationEvent integrationEvent)
        {
            await _mediator.SendCommandAsync(
                new NotifyUserCommand(
                    integrationEvent.UserId,
                    integrationEvent.Title,
                    integrationEvent.Message,
                    integrationEvent.RedirectUrl
                )
            );
        }
    }
}