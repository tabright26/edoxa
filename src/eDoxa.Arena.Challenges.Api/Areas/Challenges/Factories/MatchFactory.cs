// Filename: MatchFactory.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories
{
    public sealed class MatchFactory : IMatchFactory
    {
        private readonly IDictionary<ChallengeGame, IMatchAdapter> _adapters;

        public MatchFactory(IEnumerable<IMatchAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game);
        }

        public IMatchAdapter CreateInstance(ChallengeGame game)
        {
            if (!_adapters.TryGetValue(game, out var adapter))
            {
                throw new NotSupportedException($"The game '{game}' does not have an implementation of {nameof(IMatchAdapter)} registered as a service.");
            }

            return adapter;
        }
    }
}
