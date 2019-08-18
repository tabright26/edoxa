// Filename: BestOf.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class BestOf : ValueObject
    {
        public static readonly BestOf One = new BestOf(1);
        public static readonly BestOf Three = new BestOf(3);
        public static readonly BestOf Five = new BestOf(5);
        public static readonly BestOf Seven = new BestOf(7);

        private readonly int _bestOf;

        public BestOf(int bestOf)
        {
            _bestOf = bestOf;
        }

        public static implicit operator int(BestOf bestOf)
        {
            return bestOf._bestOf;
        }

        public override string ToString()
        {
            return _bestOf.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _bestOf;
        }
    }
}
