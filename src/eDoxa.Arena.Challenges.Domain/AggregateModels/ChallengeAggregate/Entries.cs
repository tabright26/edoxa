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
    public class Entries : ValueObject
    {
        public Entries(int entries) : this()
        {
            Value = entries;
        }

        public Entries(PayoutEntries payoutEntries) : this()
        {
            Value = Convert.ToInt32(payoutEntries * 2);
        }

        private Entries()
        {
            // Required by EF Core.
        }

        public int Value { get; private set; }

        public static implicit operator int(Entries entries)
        {
            return entries.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
