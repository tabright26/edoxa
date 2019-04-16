// Filename: RandomSettings.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class RandomSettings
    {
        private static readonly Random Random = new Random();

        public int NextEntries(int minValue = ChallengeSettings.MinEntries, int maxValue = ChallengeSettings.MaxEntries)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            // Entries is under 100$
            var multiplier = 10;

            // Entries is over 100$
            if (minValue >= 100)
            {
                multiplier = 50;
            }

            // Entries is over 1000$
            if (minValue >= 1000)
            {
                multiplier = 500;
            }

            var entries = Random.Next(minValue, maxValue + 1);

            entries = this.RoundMultiplier(entries, multiplier);

            return entries * multiplier;
        }

        public decimal NextEntryFee(decimal minValue = ChallengeSettings.MinEntryFee, decimal maxValue = ChallengeSettings.MaxEntryFee)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            const int multiplierOfCents = 100;

            var min = (int) Math.Round(minValue * multiplierOfCents);

            var max = (int) Math.Round(maxValue * multiplierOfCents);

            var entryFee = Random.Next(min, max + 1);

            // Entry fee is under 5$.
            if (entryFee < 5 * multiplierOfCents)
            {
                entryFee = this.RoundMultiplier(entryFee, multiplierOfCents);

                if (entryFee == 0)
                {
                    return 0.25M;
                }

                if (entryFee * multiplierOfCents >= max)
                {
                    return entryFee;
                }

                var cents = Random.Next(0, 4) * 0.25M;

                return entryFee + cents;
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

            entryFee = this.RoundMultiplier(entryFee, multiplier);

            return entryFee * (multiplier / multiplierOfCents);
        }

        public int NextBestOf(int minValue = ChallengeSettings.MinBestOf, int maxValue = ChallengeSettings.MaxBestOf)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            return Random.Next(minValue, maxValue + 1);
        }

        private int RoundMultiplier(decimal value, int multiplier)
        {
            return (int) Math.Round(value / multiplier);
        }
    }
}