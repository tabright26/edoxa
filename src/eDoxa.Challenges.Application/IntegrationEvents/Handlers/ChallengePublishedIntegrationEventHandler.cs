﻿// Filename: ChallengePublishedIntegrationEventHandler.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Commands.Extensions;
using eDoxa.ServiceBus;

using MediatR;

namespace eDoxa.Challenges.Application.IntegrationEvents.Handlers
{
    public sealed class ChallengePublishedIntegrationEventHandler : IIntegrationEventHandler<ChallengePublishedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public ChallengePublishedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(ChallengePublishedIntegrationEvent integrationEvent)
        {
            await _mediator.SendCommandAsync(new PublishCommand(integrationEvent.Interval));
        }
    }
}