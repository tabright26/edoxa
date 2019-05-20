// Filename: EntryFeeRandom.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain
{
    public sealed class EntryFeeRandom : SetupRandom<EntryFee, EntryFeeRange>
    {
        private static readonly System.Random Random = new System.Random();

        public override EntryFee Next(EntryFeeRange range)
        {
            const int multiplierOfCents = 100;

            var minValue = (int) Math.Round(range.MinValue * multiplierOfCents);

            var maxValue = (int) Math.Round(range.MaxValue * multiplierOfCents);

            var entryFee = Random.Next(minValue, maxValue + 1);

            // Entry fee is under 5$.
            if (entryFee < 5 * multiplierOfCents)
            {
                entryFee = (int)Math.Round((decimal)entryFee / multiplierOfCents);

                if (entryFee == 0)
                {
                    return new EntryFee(0.25M);
                }

                if (entryFee * multiplierOfCents >= maxValue)
                {
                    return new EntryFee(entryFee);
                }

                var cents = Random.Next(0, 4) * 0.25M;

                return new EntryFee(entryFee + cents);
            }

            var multiplier = 1 * multiplierOfCents;

            // Entry fee is over 5$
            if (entryFee >= 5 * multiplierOfCents)
            {
                multiplier = 5 * multiplierOfCents;
            }

            // Entry fee is over 100$
            if (entryFee >= 100 * multiplierOfCents)
            {
                multiplier = 50 * multiplierOfCents;
            }

            // Entry fee is over 1000$
            if (entryFee >= 1000 * multiplierOfCents)
            {
                multiplier = 500 * multiplierOfCents;
            }

            entryFee = (int)Math.Round((decimal)entryFee / multiplier);

            return new EntryFee(entryFee * (multiplier / multiplierOfCents));
        }
    }
}