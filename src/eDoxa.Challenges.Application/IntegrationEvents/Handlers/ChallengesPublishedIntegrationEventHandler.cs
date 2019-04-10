// Filename: ChallengesPublishedIntegrationEventHandler.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.ServiceBus;

using MediatR;

namespace eDoxa.Challenges.Application.IntegrationEvents.Handlers
{
    public sealed class ChallengesPublishedIntegrationEventHandler : IIntegrationEventHandler<ChallengesPublishedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public ChallengesPublishedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(ChallengesPublishedIntegrationEvent integrationEvent)
        {
            await _mediator.SendCommandAsync(new PublishChallengesCommand());
        }
    }
}