// Filename: RandomChallengeSetup.cs
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
using eDoxa.Challenges.Domain.Entities.Random.Ranges;

namespace eDoxa.Challenges.Domain.Entities.Random
{
    public sealed class RandomChallengeSetup : ChallengeSetup
    {
        private static readonly BestOfRandom BestOfRandom = new BestOfRandom();
        private static readonly EntriesRandom EntriesRandom = new EntriesRandom();
        private static readonly EntryFeeRandom EntryFeeRandom = new EntryFeeRandom();

        public RandomChallengeSetup(PublisherInterval interval) : base(
            NextBestOf(interval),
            NextEntries(interval),
            NextEntryFee(interval),
            PayoutRatio.DefaultValue,
            ServiceChargeRatio.DefaultValue,
            true
        )
        {
        }

        private static BestOf NextBestOf(PublisherInterval interval)
        {
            if (interval == PublisherInterval.Daily)
            {
                return BestOfRandom.Next(new BestOfRange(new BestOf(1), new BestOf(3)));
            }

            if (interval == PublisherInterval.Weekly)
            {
                return BestOfRandom.Next(new BestOfRange(new BestOf(3), new BestOf(5)));
            }

            if (interval == PublisherInterval.Monthly)
            {
                return BestOfRandom.Next(new BestOfRange(new BestOf(3), new BestOf(BestOf.Max)));
            }

            throw new ArgumentException(nameof(interval));
        }

        private static Entries NextEntries(PublisherInterval interval)
        {
            if (interval == PublisherInterval.Daily)
            {
                return EntriesRandom.Next(new EntriesRange(new Entries(30), new Entries(50)));
            }

            if (interval == PublisherInterval.Weekly)
            {
                return EntriesRandom.Next(new EntriesRange(new Entries(80), new Entries(150)));
            }

            if (interval == PublisherInterval.Monthly)
            {
                return EntriesRandom.Next(new EntriesRange(new Entries(200), new Entries(500)));
            }

            throw new ArgumentException(nameof(interval));
        }

        private static EntryFee NextEntryFee(PublisherInterval interval)
        {
            if (interval == PublisherInterval.Daily)
            {
                return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(0.25M), new EntryFee(5M)));
            }

            if (interval == PublisherInterval.Weekly)
            {
                return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(2.5M), new EntryFee(10M)));
            }

            if (interval == PublisherInterval.Monthly)
            {
                return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(10M), new EntryFee(25M)));
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}