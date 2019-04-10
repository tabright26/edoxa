﻿// Filename: CloseChallengesCommandHandler.cs
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
    public class CloseChallengesCommandHandler : AsyncCommandHandler<CloseChallengesCommand>
    {
        private readonly IChallengeCloserService _challengeCloserService;

        public CloseChallengesCommandHandler(IChallengeCloserService challengeCloserService)
        {
            _challengeCloserService = challengeCloserService ?? throw new ArgumentNullException(nameof(challengeCloserService));
        }

        protected override async Task Handle(CloseChallengesCommand command, CancellationToken cancellationToken)
        {
            await _challengeCloserService.CloseAsync();
        }
    }
}