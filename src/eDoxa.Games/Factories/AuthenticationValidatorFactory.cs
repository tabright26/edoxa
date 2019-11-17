﻿// Filename: AuthFactorValidatorFactory.cs
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
    public sealed class AuthenticationValidatorFactory : IAuthenticationValidatorFactory
    {
        private readonly IDictionary<Game, IAuthenticationValidatorAdapter> _adapters;

        public AuthenticationValidatorFactory(IEnumerable<IAuthenticationValidatorAdapter> adapters)
        {
            _adapters = adapters.ToDictionary(adapter => adapter.Game, adapter => adapter);
        }

        public IAuthenticationValidatorAdapter CreateInstance(Game game)
        {
            if (_adapters.TryGetValue(game, out var adapter))
            {
                return adapter;
            }

            throw new InvalidOperationException($"The game ({game}) is not supported at the moment.");
        }
    }
}
