﻿// Filename: EntriesRandom.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public sealed class EntriesRandom : IRandom<Entries, EntriesRange>
    {
        private static readonly Random Random = new Random();

        public Entries Next(EntriesRange range)
        {
            // Entries is under 100$
            var multiplier = 10;

            // Entries is over 100$
            if (range.MinValue >= 100)
            {
                multiplier = 50;
            }

            // Entries is over 1000$
            if (range.MinValue >= 1000)
            {
                multiplier = 500;
            }

            var entries = Random.Next(range.MinValue, range.MaxValue + 1);

            entries = Optimization.RoundMultiplier(entries, multiplier);

            return new Entries(entries * multiplier);
        }
    }
}