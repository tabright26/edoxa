// Filename: BestOfRange.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.Entities.Random
{
    public sealed class BestOfRange
    {
        public BestOfRange(BestOf minValue, BestOf maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(nameof(maxValue));
            }

            MinValue = minValue;
            MaxValue = maxValue;
        }

        public BestOf MaxValue { get; }

        public BestOf MinValue { get; }
    }
}