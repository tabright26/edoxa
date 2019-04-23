// Filename: ChallengePublisherService.cs
// Date Created: 2019-04-21
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
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Application.Services
{
    public abstract class ChallengePublisherService : IChallengePublisherService
    {
        protected static readonly ChallengePubliserFactory ChallengePubliserFactory = ChallengePubliserFactory.Instance;
        protected static readonly ChallengeScoringFactory ChallengeScoringFactory = ChallengeScoringFactory.Instance;
        protected static readonly ChallengeTimelineFactory ChallengeTimelineFactory = ChallengeTimelineFactory.Instance;

        private readonly ILogger _logger;

        protected ChallengePublisherService(ILogger logger)
        {
            _logger = logger;
        }

        protected IEnumerable<Game> Games
        {
            get
            {
                var games = Enum.GetValues(typeof(Game)).Cast<Game>().ToList();

                games.Remove(Game.None);

                games.Remove(Game.All);

                return games.ToArray();
            }
        }

        public abstract Task PublishAsync();

        protected async Task TryPublish(Func<Task> publishing)
        {
            try
            {
                await publishing();
            }
            catch (NotImplementedException exception)
            {
                _logger.LogWarning(exception, $"{nameof(ChallengeMonthlyPublisherService)} tries to publish challenges for an unsupported game.");
            }
            catch (DbUpdateException exception)
            {
                _logger.LogCritical(exception, $"{nameof(ChallengeMonthlyPublisherService)} fails to publish challenges for a supported game.");
            }
        }
    }
}