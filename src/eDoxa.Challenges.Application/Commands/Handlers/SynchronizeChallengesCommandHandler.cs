// Filename: SynchronizeChallengesCommandHandler.cs
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
    public sealed class SynchronizeChallengesCommandHandler : AsyncCommandHandler<SynchronizeChallengesCommand>
    {
        private readonly IChallengeSynchronizerService _challengeSynchronizerService;

        public SynchronizeChallengesCommandHandler(IChallengeSynchronizerService challengeSynchronizerService)
        {
            _challengeSynchronizerService = challengeSynchronizerService ?? throw new ArgumentNullException(nameof(challengeSynchronizerService));
        }

        protected override async Task Handle(SynchronizeChallengesCommand request, CancellationToken cancellationToken)
        {
            await _challengeSynchronizerService.SynchronizeAsync();
        }
    }
}