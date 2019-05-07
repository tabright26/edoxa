// Filename: EntriesRandom.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Utilities;

namespace eDoxa.Challenges.Domain.Entities.Random
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

            entries = MathUtils.RoundMultiplier(entries, multiplier);

            return new Entries(entries * multiplier);
        }
    }
}