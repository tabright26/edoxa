﻿// Filename: CompleteCommandHandler.cs
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

using eDoxa.Challenges.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    internal sealed class CompleteCommandHandler : AsyncCommandHandler<CompleteCommand>
    {
        private readonly IChallengeService _challengeService;

        public CompleteCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] CompleteCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.CompleteAsync();
        }
    }
}