// Filename: SynchronizeChallengesCommandHandler.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Domain.Providers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Handlers
{
    public sealed class SynchronizeChallengesCommandHandler : AsyncCommandHandler<SynchronizeChallengesCommand>
    {
        private readonly IChallengeService _challengeService;

        public SynchronizeChallengesCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] SynchronizeChallengesCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.SynchronizeAsync(ChallengeGame.LeagueOfLegends, TimeSpan.FromHours(1), new UtcNowDateTimeProvider(), cancellationToken);
        }
    }
}
