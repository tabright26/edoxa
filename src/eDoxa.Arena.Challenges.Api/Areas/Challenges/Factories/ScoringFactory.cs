// Filename: ScoringFactory.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Strategies;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories
{
    public sealed class ScoringFactory : IScoringFactory
    {
        private readonly IDictionary<ChallengeGame, IScoringStrategy> _strategies;

        public ScoringFactory(IEnumerable<IScoringStrategy>? strategies = null)
        {
            _strategies = strategies?.ToDictionary(strategie => strategie.Game) ??
                          Assembly.GetAssembly(typeof(Startup))
                              .GetTypes()
                              .Where(type => typeof(IScoringStrategy).IsAssignableFrom(type) && !type.IsInterface)
                              .Select(Activator.CreateInstance)
                              .Cast<IScoringStrategy>()
                              .ToDictionary(strategy => strategy.Game);
        }

        public IScoringStrategy CreateInstance(ChallengeGame game)
        {
            if (!_strategies.TryGetValue(game, out var strategie))
            {
                throw new NotSupportedException($"The game '{game}' does not have an implementation of {nameof(IScoringStrategy)} registered as a service.");
            }

            return strategie;
        }
    }
}
