// Filename: AuthFactor.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate
{
    public class AuthFactor : ValueObject
    {
        public AuthFactor(PlayerId playerId, object key)
        {
            PlayerId = playerId;
            Key = key;
        }

        public PlayerId PlayerId { get; }

        public object Key { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PlayerId;
            yield return Key;
        }

        public override string ToString()
        {
            return PlayerId;
        }
    }
}
