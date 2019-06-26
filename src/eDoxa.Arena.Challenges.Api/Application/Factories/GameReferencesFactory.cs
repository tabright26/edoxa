// Filename: GameReferencesFactory.cs
// Date Created: 2019-06-25
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

using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;

namespace eDoxa.Arena.Challenges.Api.Application.Factories
{
    public sealed class GameReferencesFactory : IGameReferencesFactory
    {
        private readonly IDictionary<ChallengeGame, IGameReferencesAdapter> _adapters;

        public GameReferencesFactory(IEnumerable<IGameReferencesAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game);
        }

        public IGameReferencesAdapter CreateInstance(ChallengeGame game)
        {
            if (!_adapters.TryGetValue(game, out var adapter))
            {
                throw new NotSupportedException(
                    $"The game '{game}' does not have an implementation of {nameof(IGameReferencesAdapter)} registered as a service."
                );
            }

            return adapter;
        }
    }
}
