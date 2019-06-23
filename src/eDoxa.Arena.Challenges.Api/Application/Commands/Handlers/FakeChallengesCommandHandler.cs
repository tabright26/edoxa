// Filename: FakeChallengesCommandHandler.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Handlers
{
    public sealed class FakeChallengesCommandHandler : AsyncCommandHandler<FakeChallengesCommand>
    {
        private readonly IChallengeService _challengeService;

        public FakeChallengesCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] FakeChallengesCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.FakeChallengesAsync(
                command.Count,
                command.Seed,
                command.Game,
                command.State,
                command.EntryFeeCurrency,
                cancellationToken
            );
        }
    }
}
