// Filename: CloseChallengesCommandHandler.cs
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

using eDoxa.Arena.Challenges.Api.Application.Abstractions.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Common;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Handlers
{
    public sealed class CloseChallengesCommandHandler : AsyncCommandHandler<CloseChallengesCommand>
    {
        private readonly IChallengeService _challengeService;

        public CloseChallengesCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] CloseChallengesCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.CloseAsync(new UtcNowDateTimeProvider(), cancellationToken);
        }
    }
}
