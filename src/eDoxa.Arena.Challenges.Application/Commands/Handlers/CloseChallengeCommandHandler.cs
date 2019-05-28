// Filename: CompleteChallengeCommandHandler.cs
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
    internal sealed class CloseChallengeCommandHandler : AsyncCommandHandler<CloseChallengeCommand>
    {
        private readonly IChallengeService _challengeService;

        public CloseChallengeCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] CloseChallengeCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.CompleteAsync(command.ChallengeId, cancellationToken);
        }
    }
}
