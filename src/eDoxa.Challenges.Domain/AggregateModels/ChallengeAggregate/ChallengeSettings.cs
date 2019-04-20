// Filename: ChallengeSettings.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;

using eDoxa.Challenges.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class ChallengeSettings : ValueObject
    {
        private BestOf _bestOf;
        private Entries _entries;
        private EntryFee _entryFee;
        private bool _generated;
        private PayoutRatio _payoutRatio;
        private ServiceChargeRatio _serviceChargeRatio;
        private ChallengeType _type;

        internal ChallengeSettings(
            BestOf bestOf,
            Entries entries,
            EntryFee entryFee,
            PayoutRatio payoutRatio,
            ServiceChargeRatio serviceChargeRatio) : this(bestOf, entries, entryFee)
        {
            _payoutRatio = payoutRatio;
            _serviceChargeRatio = serviceChargeRatio;
        }

        internal ChallengeSettings(BestOf bestOf, Entries entries, EntryFee entryFee) : this()
        {
            _bestOf = bestOf;
            _entries = entries;
            _entryFee = entryFee;
        }

        internal ChallengeSettings(ChallengePublisherPeriodicity periodicity) : this()
        {
            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:

                    _bestOf = BestOf.Random(new BestOf(1), new BestOf(3));

                    _entries = Entries.Random(new Entries(30), new Entries(50));

                    _entryFee = EntryFee.Random(new EntryFee(0.25M), new EntryFee(5M));

                    break;

                case ChallengePublisherPeriodicity.Weekly:

                    _bestOf = BestOf.Random(new BestOf(3), new BestOf(5));

                    _entries = Entries.Random(new Entries(75), new Entries(150));

                    _entryFee = EntryFee.Random(new EntryFee(2.5M), new EntryFee(10M));

                    break;

                case ChallengePublisherPeriodicity.Monthly:

                    _bestOf = BestOf.Random(new BestOf(3), new BestOf(BestOf.Max));

                    _entries = Entries.Random(new Entries(200), new Entries(500));

                    _entryFee = EntryFee.Random(new EntryFee(10M), new EntryFee(25M));

                    break;
            }

            _generated = true;
        }

        internal ChallengeSettings()
        {
            _type = ChallengeType.Default;
            _bestOf = BestOf.DefaultValue;
            _entries = Entries.DefaultValue;
            _entryFee = EntryFee.DefaultValue;
            _payoutRatio = PayoutRatio.DefaultValue;
            _serviceChargeRatio = ServiceChargeRatio.DefaultValue;
            _generated = false;
        }

        public ChallengeType Type
        {
            get => _type;
            private set
            {
                if (!Enum.IsDefined(typeof(ChallengeType), value))
                {
                    throw new InvalidEnumArgumentException(nameof(Type), (int) value, typeof(ChallengeType));
                }

                if (value == ChallengeType.None || value == ChallengeType.All)
                {
                    throw new ArgumentException(nameof(Type));
                }

                _type = value;
            }
        }

        public bool Generated => _generated;

        public BestOf BestOf => _bestOf;

        public Entries Entries => _entries;

        public EntryFee EntryFee => _entryFee;

        public PayoutRatio PayoutRatio => _payoutRatio;

        public ServiceChargeRatio ServiceChargeRatio => _serviceChargeRatio;

        public PayoutEntries PayoutEntries => new PayoutEntries(_entries, _payoutRatio);

        public PrizePool PrizePool => new PrizePool(_entries, _entryFee, _serviceChargeRatio);
    }
}