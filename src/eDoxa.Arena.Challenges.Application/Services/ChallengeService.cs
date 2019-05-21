// Filename: ChallengeService.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            var challenges = await _challengeRepository.FindChallengesAsync(Game.All);

            challenges.ForEach(challenge => challenge.Complete());

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }

        public Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}