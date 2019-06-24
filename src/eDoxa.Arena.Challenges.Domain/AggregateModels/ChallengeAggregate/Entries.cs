// Filename: Entries.cs
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

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Entries : ValueObject
    {
        private readonly int _entries;

        public Entries(PayoutEntries payoutEntries) : this(Convert.ToInt32(payoutEntries * 2))
        {
        }

        public Entries(int entries)
        {
            _entries = entries;
        }

        public static implicit operator int(Entries entries)
        {
            return entries._entries;
        }

        public override string ToString()
        {
            return _entries.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _entries;
        }
    }
}
