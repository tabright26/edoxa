// Filename: ChallengeService.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Entities;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Default.Factories;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private static readonly PubliserFactory PubliserFactory = PubliserFactory.Instance;
        private static readonly ScoringFactory ScoringFactory = ScoringFactory.Instance;
        private static readonly DefaultTimelineFactory DefaultTimelineFactory = DefaultTimelineFactory.Instance;
        private readonly IChallengeRepository _challengeRepository;

        private readonly ILogger<ChallengeService> _logger;

        public ChallengeService(ILogger<ChallengeService> logger, IChallengeRepository challengeRepository)
        {
            _logger = logger;
            _challengeRepository = challengeRepository;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            var challenges = await _challengeRepository.FindChallengesAsync(ChallengeType.All, Game.All, ChallengeState.Ended);

            challenges.ForEach(challenge => challenge.Complete());

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }

        public async Task PublishAsync(PublisherInterval interval, CancellationToken cancellationToken)
        {
            foreach (var game in Game.GetAll())
            {
                // TODO: Refactor this try catch.
                try
                {
                    var challenges = PubliserFactory.CreatePublisherStrategy(interval, game).Challenges.ToList();

                    foreach (var challenge in challenges)
                    {
                        challenge.Publish(ScoringFactory.CreateScoringStrategy(challenge), DefaultTimelineFactory.CreateTimelineStrategy(interval));

                        _challengeRepository.Create(challenge);
                    }

                    await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
                }
                catch (NotImplementedException exception)
                {
                    _logger.LogWarning(exception, $"{nameof(ChallengeService)} tries to publish challenges for an unsupported game.");
                }
                catch (ArgumentException exception)
                {
                    _logger.LogWarning(exception, $"{nameof(ChallengeService)} tries to publish challenges for an unsupported game.");
                }
                catch (DbUpdateException exception)
                {
                    _logger.LogCritical(exception, $"{nameof(ChallengeService)} fails to publish challenges for a supported game.");
                }
            }
        }

        public async Task SynchronizeAsync(Game game, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}