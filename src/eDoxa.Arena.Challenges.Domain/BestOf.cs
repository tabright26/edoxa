﻿// Filename: BestOf.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class BestOf : ValueObject
    {
        public static readonly BestOf One = new BestOf(1);
        public static readonly BestOf Three = new BestOf(3);
        public static readonly BestOf Five = new BestOf(5);
        public static readonly BestOf Seven = new BestOf(7);

        public BestOf(int bestOf) : this()
        {
            Value = bestOf;
        }

        private BestOf()
        {
            // Required by EF Core.
        }

        public int Value { get; private set; }

        public static implicit operator int(BestOf bestOf)
        {
            return bestOf.Value;
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
