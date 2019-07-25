// Filename: SynchronizeChallengesRequestHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain.Providers;

using JetBrains.Annotations;

using MediatR;

namespace eDoxa.Arena.Challenges.Api.Application.Requests.Handlers
{
    public sealed class SynchronizeChallengesRequestHandler : AsyncRequestHandler<SynchronizeChallengesRequest>
    {
        private readonly IChallengeService _challengeService;

        public SynchronizeChallengesRequestHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        protected override async Task Handle([NotNull] SynchronizeChallengesRequest request, CancellationToken cancellationToken)
        {
            await _challengeService.SynchronizeAsync(ChallengeGame.LeagueOfLegends, TimeSpan.FromHours(1), new UtcNowDateTimeProvider(), cancellationToken);
        }
    }
}
