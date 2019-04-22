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

        public RandomChallengeSetup(ChallengePublisherPeriodicity periodicity) : base(
            NextBestOf(periodicity),
            NextEntries(periodicity),
            NextEntryFee(periodicity),
            PayoutRatio.DefaultValue,
            ServiceChargeRatio.DefaultValue,
            true
        )
        {
        }

        private static BestOf NextBestOf(ChallengePublisherPeriodicity periodicity)
        {
            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:

                    return BestOfRandom.Next(new BestOfRange(new BestOf(1), new BestOf(3)));

                case ChallengePublisherPeriodicity.Weekly:

                    return BestOfRandom.Next(new BestOfRange(new BestOf(3), new BestOf(5)));

                case ChallengePublisherPeriodicity.Monthly:

                    return BestOfRandom.Next(new BestOfRange(new BestOf(3), new BestOf(BestOf.Max)));

                default:

                    throw new ArgumentException(nameof(periodicity));
            }
        }

        private static Entries NextEntries(ChallengePublisherPeriodicity periodicity)
        {
            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:

                    return EntriesRandom.Next(new EntriesRange(new Entries(30), new Entries(50)));

                case ChallengePublisherPeriodicity.Weekly:

                    return EntriesRandom.Next(new EntriesRange(new Entries(75), new Entries(150)));

                case ChallengePublisherPeriodicity.Monthly:

                    return EntriesRandom.Next(new EntriesRange(new Entries(200), new Entries(500)));

                default:

                    throw new ArgumentException(nameof(periodicity));
            }
        }

        private static EntryFee NextEntryFee(ChallengePublisherPeriodicity periodicity)
        {
            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:

                    return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(0.25M), new EntryFee(5M)));

                case ChallengePublisherPeriodicity.Weekly:

                    return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(2.5M), new EntryFee(10M)));

                case ChallengePublisherPeriodicity.Monthly:

                    return EntryFeeRandom.Next(new EntryFeeRange(new EntryFee(10M), new EntryFee(25M)));

                default:

                    throw new ArgumentException(nameof(periodicity));
            }
        }
    }
}