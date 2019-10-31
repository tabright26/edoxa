// Filename: GameReferencesFactory.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories
{
    public sealed class GameReferencesFactory : IGameReferencesFactory
    {
        private readonly IDictionary<Game, IGameReferencesAdapter> _adapters;

        public GameReferencesFactory(IEnumerable<IGameReferencesAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game);
        }

        public IGameReferencesAdapter CreateInstance(Game game)
        {
            if (!_adapters.TryGetValue(game, out var adapter))
            {
                throw new NotSupportedException(
                    $"The game '{game}' does not have an implementation of {nameof(IGameReferencesAdapter)} registered as a service.");
            }

            return adapter;
        }
    }
}
