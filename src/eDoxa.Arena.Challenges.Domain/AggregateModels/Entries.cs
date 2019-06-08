// Filename: Entries.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public class Entries : ValueObject
    {
        public Entries(int entries) : this()
        {
            Value = entries;
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
