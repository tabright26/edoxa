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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private static readonly PubliserFactory PubliserFactory = PubliserFactory.Instance;
        private static readonly ScoringFactory ScoringFactory = ScoringFactory.Instance;
        private static readonly TimelineFactory TimelineFactory = TimelineFactory.Instance;
        private readonly IChallengeRepository _challengeRepository;

        private readonly ILogger<ChallengeService> _logger;

        public ChallengeService(ILogger<ChallengeService> logger, IChallengeRepository challengeRepository)
        {
            _logger = logger;
            _challengeRepository = challengeRepository;
        }

        // TODO: Create a enumerable class for games.
        private static IEnumerable<Game> Games
        {
            get
            {
                var games = Enum.GetValues(typeof(Game)).Cast<Game>().ToList();

                games.Remove(Game.None);

                games.Remove(Game.All);

                return games.ToArray();
            }
        }

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            var challenges = await _challengeRepository.FindChallengesAsync(Game.All, ChallengeType.All, ChallengeState1.Ended);

            challenges.ForEach(challenge => challenge.Complete());

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }

        public async Task PublishAsync(PublisherInterval interval, CancellationToken cancellationToken)
        {
            foreach (var game in Games)
            {
                // TODO: Refactor this try catch.
                try
                {
                    var challenges = PubliserFactory.CreatePublisherStrategy(interval, game).Challenges.ToList();

                    foreach (var challenge in challenges)
                    {
                        challenge.Publish(ScoringFactory.CreateScoringStrategy(challenge), TimelineFactory.CreateTimelineStrategy(interval));

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
        }
    }
}