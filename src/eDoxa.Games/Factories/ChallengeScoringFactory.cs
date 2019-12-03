// Filename: ChallengeScoringFactory.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Games.Abstractions.Adapters;
using eDoxa.Games.Abstractions.Factories;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Factories
{
    public sealed class ChallengeScoringFactory : IChallengeScoringFactory
    {
        private readonly IDictionary<Game, IChallengeScoringAdapter> _adapters;

        public ChallengeScoringFactory(IEnumerable<IChallengeScoringAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game, adapter => adapter);
        }

        public IChallengeScoringAdapter CreateInstance(Game game)
        {
            if (_adapters.TryGetValue(game, out var adapter))
            {
                return adapter;
            }

            throw new InvalidOperationException($"The game ({game}) is not supported at the moment.");
        }
    }
}
