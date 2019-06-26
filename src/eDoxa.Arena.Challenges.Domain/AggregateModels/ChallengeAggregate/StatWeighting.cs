// Filename: StatWeighting.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatWeighting : ValueObject
    {
        private readonly float _weighting;

        public StatWeighting(float weighting)
        {
            _weighting = weighting;
        }

        public static implicit operator float(StatWeighting weighting)
        {
            return weighting._weighting;
        }

        public override string ToString()
        {
            return _weighting.ToString("R");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _weighting;
        }
    }
}
