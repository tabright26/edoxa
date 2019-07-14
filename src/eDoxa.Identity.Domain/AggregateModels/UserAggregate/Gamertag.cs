// Filename: Gamertag.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class Gamertag : ValueObject
    {
        private readonly string _gamertag;

        public Gamertag(string gamertag)
        {
            _gamertag = gamertag;
        }

        public static implicit operator string(Gamertag gamertag)
        {
            return gamertag._gamertag;
        }

        public override string ToString()
        {
            return _gamertag;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _gamertag;
        }
    }
}
