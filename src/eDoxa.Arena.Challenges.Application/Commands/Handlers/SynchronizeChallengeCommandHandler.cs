// Filename: SynchronizeChallengeCommandHandler.cs
// Date Created: 2019-05-20
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
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    public sealed class SynchronizeChallengeCommandHandler : AsyncCommandHandler<SynchronizeChallengeCommand>
    {
        private readonly IChallengeService _challengeService;

        public SynchronizeChallengeCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] SynchronizeChallengeCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.SynchronizeAsync(command.ChallengeId, cancellationToken);
        }
    }
}
