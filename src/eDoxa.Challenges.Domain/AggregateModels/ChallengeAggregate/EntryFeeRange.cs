// Filename: EntryFeeRange.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
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