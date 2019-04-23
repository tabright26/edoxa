// Filename: ChallengeCloserService.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Application.Services
{
    public class ChallengeCloserService : IChallengeCloserService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeCloserService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task CloseAsync()
        {
            var challenges = await _challengeRepository.FindChallengesAsync(Game.All, ChallengeType.All, ChallengeState1.Ended);

            foreach (var challenge in challenges)
            {
                challenge.Close();
            }

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync();
        }
    }
}