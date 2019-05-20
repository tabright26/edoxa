// Filename: SynchronizeCommandHandler.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    internal sealed class SynchronizeCommandHandler : AsyncCommandHandler<SynchronizeCommand>
    {
        private readonly IChallengeService _challengeSynchronizerService;

        public SynchronizeCommandHandler(IChallengeService challengeSynchronizerService)
        {
            _challengeSynchronizerService = challengeSynchronizerService;
        }

        protected override async Task Handle([NotNull] SynchronizeCommand request, CancellationToken cancellationToken)
        {
            await _challengeSynchronizerService.SynchronizeAsync(request.Game, cancellationToken);
        }
    }
}