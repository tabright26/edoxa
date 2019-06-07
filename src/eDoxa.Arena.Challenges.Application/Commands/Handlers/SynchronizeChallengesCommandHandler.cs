// Filename: SynchronizeChallengesCommandHandler.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Seedwork.Application.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
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
            await _challengeService.SynchronizeAsync(Game.LeagueOfLegends, cancellationToken);
        }
    }
}
