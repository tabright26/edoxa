// Filename: Password.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public abstract class Password : ValueObject
    {
        protected Password(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; }

        public sealed override string ToString()
        {
            return Hash;
        }

        protected sealed override IEnumerable<object> GetAtomicValues()
        {
            yield return Hash;
        }
    }
}
