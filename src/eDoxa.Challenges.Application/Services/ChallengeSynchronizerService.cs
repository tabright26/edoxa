// Filename: ChallengeSynchronizerService.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;

namespace eDoxa.Challenges.Application.Services
{
    public class ChallengeSynchronizerService : IChallengeSynchronizerService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeSynchronizerService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository ?? throw new ArgumentNullException(nameof(challengeRepository));
        }

        public async Task SynchronizeAsync()
        {
            await Task.CompletedTask;
        }
    }
}