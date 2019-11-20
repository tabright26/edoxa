// Filename: AuthFactorGeneratorFactory.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Abstractions.Factories;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Games.Factories
{
    public sealed class GameGameAuthenticationGeneratorFactory : IGameAuthenticationGeneratorFactory
    {
        private readonly IDictionary<Game, IAuthenticationGeneratorAdapter> _adapters;

        public GameGameAuthenticationGeneratorFactory(IEnumerable<IAuthenticationGeneratorAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game, adapter => adapter);
        }

        public IAuthenticationGeneratorAdapter CreateInstance(Game game)
        {
            if (_adapters.TryGetValue(game, out var adapter))
            {
                return adapter;
            }

            throw new InvalidOperationException($"The game ({game}) is not supported at the moment.");
        }
    }
}
