// Filename: PublishChallengesCommandHandler.cs
// Date Created: 2019-04-21
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
    internal sealed class PublishChallengesCommandHandler : AsyncCommandHandler<PublishChallengesCommand>
    {
        private readonly IChallengeMonthlyPublisherService _challengeMonthlyPublisherService;

        public PublishChallengesCommandHandler(IChallengeMonthlyPublisherService challengeMonthlyPublisherService)
        {
            _challengeMonthlyPublisherService = challengeMonthlyPublisherService;
        }

        protected override async Task Handle([NotNull] PublishChallengesCommand command, CancellationToken cancellationToken)
        {
            await _challengeMonthlyPublisherService.PublishAsync();
        }
    }
}