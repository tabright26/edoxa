﻿// Filename: PublishChallengesCommandHandler.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.Seedwork.Application.Commands.Handlers;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    public class PublishChallengesCommandHandler : AsyncCommandHandler<PublishChallengesCommand>
    {
        private readonly IChallengeMonthlyPublisherService _challengeMonthlyPublisherService;

        public PublishChallengesCommandHandler(IChallengeMonthlyPublisherService challengeMonthlyPublisherService)
        {
            _challengeMonthlyPublisherService = challengeMonthlyPublisherService ?? throw new ArgumentNullException(nameof(challengeMonthlyPublisherService));
        }

        protected override async Task Handle(PublishChallengesCommand command, CancellationToken cancellationToken)
        {
            await _challengeMonthlyPublisherService.PublishAsync();
        }
    }
}