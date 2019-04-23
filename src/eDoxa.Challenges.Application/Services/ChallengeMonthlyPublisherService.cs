// Filename: ChallengeMonthlyPublisherService.cs
// Date Created: 2019-03-22
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

using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Application.Services
{
    public sealed partial class ChallengeMonthlyPublisherService : ChallengePublisherService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeMonthlyPublisherService(ILogger logger, IChallengeRepository challengeRepository) : base(logger)
        {
            _challengeRepository = challengeRepository;
        }
    }

    public sealed partial class ChallengeMonthlyPublisherService : IChallengeMonthlyPublisherService
    {
        public override async Task PublishAsync()
        {
            foreach (var game in Games)
            {
                await this.TryPublish(
                    async () =>
                    {
                        var strategy = Factory.Create(ChallengeInterval.Monthly, game);

                        _challengeRepository.Create(strategy.Challenges);

                        await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync();
                    }
                );
            }
        }
    }
}