// Filename: ChallengeSetupRandom.cs
// Date Created: 2019-04-22
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
    public sealed class RandomChallengeSetup : ChallengeSetup
    {
        private static readonly BestOfRandom BestOfRandom = new BestOfRandom();
        private static readonly EntriesRandom EntriesRandom = new EntriesRandom();
        private static readonly EntryFeeRandom EntryFeeRandom = new EntryFeeRandom();

        public RandomChallengeSetup(ChallengeInterval interval) : base(
            NextBestOf(interval),
            NextEntries(interval),
            NextEntryFee(interval),
            PayoutRatio.DefaultValue,
            ServiceChargeRatio.DefaultValue,
            true
        )
        {
        }

        private static BestOf NextBestOf(ChallengeInterval interval)
        {
            if (interval == ChallengeInterval.Daily)
            {
                return BestOfRandom.Next(new BestOfRange(new BestOf(1), new BestOf(3)));
            }

            if (interval == ChallengeInterval.Weekly)
            {
                return BestOfRandom.Next(new BestOfRange(new BestOf(3), new BestOf(5)));
            }

            if (interval == ChallengeInterval.Monthly)
            {
                return BestOfRandom.Next(new BestOfRange(new BestOf(3), new BestOf(BestOf.Max)));
            }

            throw new ArgumentException(nameof(interval));
        }

        private static Entries NextEntries(ChallengeInterval interval)
        {
            if (interval == ChallengeInterval.Daily)
            {
                return EntriesRandom.Next(new EntriesRange(new Entries(30), new Entries(50)));
            }

            if (interval == ChallengeInterval.Weekly)
            {
                return EntriesRandom.Next(new EntriesRange(new Entries(80), new Entries(150)));
            }

            if (interval == ChallengeInterval.Monthly)
            {
                return EntriesRandom.Next(new EntriesRange(new Entries(200), new Entries(500)));
            }

            throw new ArgumentException(nameof(interval));
        }

        private static EntryFee NextEntryFee(ChallengeInterval interval)
        {
            if (interval == ChallengeInterval.Daily)
            {
                return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(0.25M), new EntryFee(5M)));
            }

            if (interval == ChallengeInterval.Weekly)
            {
                return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(2.5M), new EntryFee(10M)));
            }

            if (interval == ChallengeInterval.Monthly)
            {
                return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(10M), new EntryFee(25M)));
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}