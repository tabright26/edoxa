// Filename: EntryFeeRange.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Entities.Random.Ranges
{
    public sealed class EntryFeeRange
    {
        public EntryFeeRange(EntryFee minValue, EntryFee maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(nameof(maxValue));
            }

            MinValue = minValue;
            MaxValue = maxValue;
        }

        public EntryFee MinValue { get; }

        public EntryFee MaxValue { get; }
    }
}