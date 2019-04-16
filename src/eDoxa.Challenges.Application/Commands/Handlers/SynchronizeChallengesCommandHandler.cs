// Filename: SynchronizeChallengesCommandHandler.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.Seedwork.Application.Commands.Handlers;
using JetBrains.Annotations;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    public sealed class SynchronizeChallengesCommandHandler : AsyncCommandHandler<SynchronizeChallengesCommand>
    {
        private readonly IChallengeSynchronizerService _challengeSynchronizerService;

        public SynchronizeChallengesCommandHandler(IChallengeSynchronizerService challengeSynchronizerService)
        {
            _challengeSynchronizerService = challengeSynchronizerService;
        }

        protected override async Task Handle([NotNull] SynchronizeChallengesCommand request, CancellationToken cancellationToken)
        {
            await _challengeSynchronizerService.SynchronizeAsync();
        }
    }
}