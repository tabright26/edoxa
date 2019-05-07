// Filename: EntriesRange.cs
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
    public sealed class EntriesRange
    {
        public EntriesRange(Entries minValue, Entries maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(nameof(maxValue));
            }

            MinValue = minValue;
            MaxValue = maxValue;
        }

        public Entries MinValue { get; }

        public Entries MaxValue { get; }
    }
}