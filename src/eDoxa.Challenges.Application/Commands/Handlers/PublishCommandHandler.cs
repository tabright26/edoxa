﻿// Filename: PublishCommandHandler.cs
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
    internal sealed class PublishCommandHandler : AsyncCommandHandler<PublishCommand>
    {
        private readonly IChallengeService _challengeService;

        public PublishCommandHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] PublishCommand command, CancellationToken cancellationToken)
        {
            await _challengeService.PublishAsync(command.Interval, cancellationToken);
        }
    }
}