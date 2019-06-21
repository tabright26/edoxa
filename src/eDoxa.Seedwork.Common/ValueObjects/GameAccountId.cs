// Filename: GameAccountId.cs
// Date Created: 2019-06-12
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

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Common.ValueObjects
{
    public sealed class GameAccountId : ValueObject
    {
        private readonly string _gameAccountId;

        public GameAccountId(string gameAccountId)
        {
            if (string.IsNullOrWhiteSpace(gameAccountId) || !gameAccountId.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(gameAccountId));
            }

            _gameAccountId = gameAccountId;
        }

        public static implicit operator string(GameAccountId gameAccountId)
        {
            return gameAccountId.ToString();
        }

        public static implicit operator GameAccountId(string gameAccountId)
        {
            return new GameAccountId(gameAccountId);
        }

        public override string ToString()
        {
            return _gameAccountId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _gameAccountId;
        }
    }
}
