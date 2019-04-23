// Filename: ChallengeDailyPublisherService.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;

using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Application.Services
{
    public sealed partial class ChallengeDailyPublisherService : ChallengePublisherService
    {
        private static readonly ChallengeInterval Interval = ChallengeInterval.Daily;

        private readonly IChallengeRepository _challengeRepository;

        public ChallengeDailyPublisherService(ILogger logger, IChallengeRepository challengeRepository) : base(logger)
        {
            _challengeRepository = challengeRepository;
        }
    }

    public sealed partial class ChallengeDailyPublisherService : IChallengeDailyPublisherService
    {
        public override async Task PublishAsync()
        {
            foreach (var game in Games)
            {
                await this.TryPublish(
                    async () =>
                    {
                        var challenges = ChallengePubliserFactory.CreatePublisherStrategy(Interval, game).Challenges.ToList();

                        foreach (var challenge in challenges)
                        {
                            challenge.Publish(ChallengeScoringFactory.CreateScoringStrategy(challenge),
                                ChallengeTimelineFactory.CreateTimelineStrategy(Interval));

                            _challengeRepository.Create(challenge);
                        }

                        await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync();
                    }
                );
            }
        }
    }
}