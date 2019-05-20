﻿// Filename: EntriesRandom.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class EntriesRandom : SetupRandom<Entries, EntriesRange>
    {
        private static readonly System.Random Random = new System.Random();

        public override Entries Next(EntriesRange range)
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

            entries = (int)Math.Round((decimal) entries / multiplier);

            return new Entries(entries * multiplier);
        }
    }
}