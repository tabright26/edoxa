// Filename: AuthFactorValidatorFactory.cs
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
    public sealed class AuthFactorValidatorFactory : IAuthFactorValidatorFactory
    {
        private readonly IDictionary<Game, IAuthFactorValidatorAdapter> _adapters;

        public AuthFactorValidatorFactory(IEnumerable<IAuthFactorValidatorAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game, adapter => adapter);
        }

        public IAuthFactorValidatorAdapter CreateInstance(Game game)
        {
            if (_adapters.TryGetValue(game, out var adapter))
            {
                return adapter;
            }

            throw new InvalidOperationException($"The game ({game}) is not supported at the moment.");
        }
    }
}
