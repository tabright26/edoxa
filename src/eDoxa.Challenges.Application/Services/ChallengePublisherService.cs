// Filename: ChallengePublisherService.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Application.Services
{
    public abstract class ChallengePublisherService : IChallengePublisherService
    {
        protected static readonly ChallengePubliserFactory Factory = ChallengePubliserFactory.Instance;

        private readonly ILogger _logger;

        protected ChallengePublisherService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract Task PublishAsync();

        protected Game[] Games
        {
            get
            {
                var games = Enum.GetValues(typeof(Game)).Cast<Game>().ToList();

                games.Remove(Game.None);

                games.Remove(Game.All);

                return games.ToArray();
            }
        }

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