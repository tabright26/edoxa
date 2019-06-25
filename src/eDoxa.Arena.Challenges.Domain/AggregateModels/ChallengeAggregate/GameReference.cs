// Filename: GameReference.cs
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
    public sealed class GameReference : ValueObject
    {
        private readonly string _gameReference;

        public GameReference(string gameReference)
        {
            if (string.IsNullOrWhiteSpace(gameReference) || !gameReference.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(gameReference));
            }

            _gameReference = gameReference;
        }

        public GameReference(long gameReference)
        {
            if (gameReference < 0)
            {
                throw new ArgumentException(nameof(gameReference));
            }

            _gameReference = gameReference.ToString();
        }

        public GameReference(Guid gameReference)
        {
            if (gameReference == Guid.Empty)
            {
                throw new ArgumentException(nameof(gameReference));
            }

            _gameReference = gameReference.ToString();
        }

        public static implicit operator GameReference(string gameReference)
        {
            return new GameReference(gameReference);
        }

        public static implicit operator GameReference(long gameReference)
        {
            return new GameReference(gameReference);
        }

        public static implicit operator GameReference(Guid gameReference)
        {
            return new GameReference(gameReference);
        }

        public static implicit operator string(GameReference gameReference)
        {
            return gameReference._gameReference;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _gameReference;
        }

        public override string ToString()
        {
            return _gameReference;
        }
    }
}
