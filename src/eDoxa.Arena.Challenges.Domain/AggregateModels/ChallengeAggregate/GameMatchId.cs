// Filename: GameMatchId.cs
// Date Created: 2019-06-20
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

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class GameMatchId : ValueObject
    {
        private readonly string _gameMatchId;

        public GameMatchId(string gameMatchId)
        {
            if (string.IsNullOrWhiteSpace(gameMatchId) || !gameMatchId.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(gameMatchId));
            }

            _gameMatchId = gameMatchId;
        }

        public GameMatchId(long gameMatchId)
        {
            if (gameMatchId < 0)
            {
                throw new ArgumentException(nameof(gameMatchId));
            }

            _gameMatchId = gameMatchId.ToString();
        }

        public GameMatchId(Guid gameMatchId)
        {
            if (gameMatchId == Guid.Empty)
            {
                throw new ArgumentException(nameof(gameMatchId));
            }

            _gameMatchId = gameMatchId.ToString();
        }

        public static implicit operator string(GameMatchId gameMatchId)
        {
            return gameMatchId.ToString();
        }

        public static implicit operator GameMatchId(string gameMatchId)
        {
            return new GameMatchId(gameMatchId);
        }

        public static implicit operator GameMatchId(long gameMatchId)
        {
            return new GameMatchId(gameMatchId);
        }

        public static implicit operator GameMatchId(Guid gameMatchId)
        {
            return new GameMatchId(gameMatchId);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _gameMatchId;
        }

        public override string ToString()
        {
            return _gameMatchId;
        }
    }
}
