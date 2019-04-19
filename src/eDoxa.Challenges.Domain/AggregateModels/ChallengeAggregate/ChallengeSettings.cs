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

        internal ChallengeSettings(int bestOf, int entries, decimal entryFee, float payoutRatio, float serviceChargeRatio) : this(bestOf, entries, entryFee)
        {
            _payoutRatio = new PayoutRatio(payoutRatio);
            _serviceChargeRatio = new ServiceChargeRatio(serviceChargeRatio);
        }

        internal ChallengeSettings(int bestOf, int entries, decimal entryFee) : this()
        {
            _bestOf = new BestOf(bestOf);
            _entries = new Entries(entries);
            _entryFee = new EntryFee(entryFee);
        }

        internal ChallengeSettings(ChallengePublisherPeriodicity periodicity) : this()
        {
            var random = new RandomSettings();

            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:
                    _bestOf = new BestOf(random.NextBestOf(1, 3));
                    _entries = new Entries(random.NextEntries(30, 50));
                    _entryFee = new EntryFee(random.NextEntryFee(0.25M, 5M));

                    break;
                case ChallengePublisherPeriodicity.Weekly:
                    _bestOf = new BestOf(random.NextBestOf(3, 5));
                    _entries = new Entries(random.NextEntries(75, 150));
                    _entryFee = new EntryFee(random.NextEntryFee(2.5M, 10M));

                    break;
                case ChallengePublisherPeriodicity.Monthly:
                    _bestOf = new BestOf(random.NextBestOf(3));
                    _entries = new Entries(random.NextEntries(200, 500));
                    _entryFee = new EntryFee(random.NextEntryFee(10M, 25M));

                    break;
            }

            _generated = true;
        }

        internal ChallengeSettings()
        {
            _type = ChallengeType.Default;
            _bestOf = BestOf.Default;
            _entries = Entries.Default;
            _entryFee = EntryFee.Default;
            _payoutRatio = PayoutRatio.Default;
            _serviceChargeRatio = ServiceChargeRatio.Default;
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