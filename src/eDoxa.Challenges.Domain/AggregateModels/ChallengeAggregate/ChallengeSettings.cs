// Filename: ChallengeSettings.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class ChallengeSettings
    {
        internal const int MinBestOf = 1;
        internal const int MaxBestOf = 7;
        internal const int DefaultBestOf = 3;
        internal const int MinEntries = 30;
        internal const int MaxEntries = 2000;
        internal const int DefaultEntries = 50;
        internal const decimal MinEntryFee = 0.25M;
        internal const decimal MaxEntryFee = 1500M;
        internal const decimal DefaultEntryFee = 5M;
        internal const float MinPayoutRatio = 0.25F;
        internal const float MaxPayoutRatio = 0.75F;
        internal const float DefaultPayoutRatio = 0.5F;
        internal const float MinServiceChargeRatio = 0.1F;
        internal const float MaxServiceChargeRatio = 0.3F;
        internal const float DefaultServiceChargeRatio = 0.2F;
    }

    public sealed partial class ChallengeSettings : ValueObject
    {
        private static readonly ChallengeHelper _helper = new ChallengeHelper();

        private ChallengeType _type;
        private int _bestOf;
        private int _entries;
        private decimal _entryFee;
        private float _payoutRatio;
        private float _serviceChargeRatio;
        private bool _generated;

        internal ChallengeSettings(int bestOf, int entries, decimal entryFee, float payoutRatio, float serviceChargeRatio) : this(bestOf, entries, entryFee)
        {
            PayoutRatio = payoutRatio;
            ServiceChargeRatio = serviceChargeRatio;
        }

        internal ChallengeSettings(int bestOf, int entries, decimal entryFee) : this()
        {
            BestOf = bestOf;
            Entries = entries;
            EntryFee = entryFee;
        }

        internal ChallengeSettings(ChallengePublisherPeriodicity periodicity) : this()
        {
            var random = new RandomSettings();

            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:
                    _bestOf = random.NextBestOf(1, 3);
                    _entries = random.NextEntries(30, 50);
                    _entryFee = random.NextEntryFee(0.25M, 5M);
                    break;
                case ChallengePublisherPeriodicity.Weekly:
                    _bestOf = random.NextBestOf(3, 5);
                    _entries = random.NextEntries(75, 150);
                    _entryFee = random.NextEntryFee(2.5M, 10M);
                    break;
                case ChallengePublisherPeriodicity.Monthly:
                    _bestOf = random.NextBestOf(3);
                    _entries = random.NextEntries(200, 500);
                    _entryFee = random.NextEntryFee(10M, 25M);
                    break;
            }

            _generated = true;
        }

        internal ChallengeSettings()
        {
            _type = ChallengeType.Default;
            _bestOf = DefaultBestOf;
            _entries = DefaultEntries;
            _entryFee = DefaultEntryFee;
            _payoutRatio = DefaultPayoutRatio;
            _serviceChargeRatio = DefaultServiceChargeRatio;
            _generated = false;
        }

        public ChallengeType Type
        {
            get
            {
                return _type;
            }
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

        public int BestOf
        {
            get
            {
                return _bestOf;
            }
            private set
            {
                if (value < MinBestOf)
                {
                    throw new ArgumentOutOfRangeException(nameof(BestOf));
                }

                if (value > MaxBestOf)
                {
                    throw new ArgumentOutOfRangeException(nameof(BestOf));
                }

                _bestOf = value;
            }
        }

        public int Entries
        {
            get
            {
                return _entries;
            }
            private set
            {
                if (value < MinEntries)
                {
                    throw new ArgumentOutOfRangeException(nameof(Entries));
                }

                if (value > MaxEntries)
                {
                    throw new ArgumentOutOfRangeException(nameof(Entries));
                }

                if (value % 10 != 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Entries));
                }

                _entries = value;
            }
        }

        public decimal EntryFee
        {
            get
            {
                return _entryFee;
            }
            private set
            {
                if (value < MinEntryFee)
                {
                    throw new ArgumentOutOfRangeException(nameof(EntryFee));
                }

                if (value > MaxEntryFee)
                {
                    throw new ArgumentOutOfRangeException(nameof(EntryFee));
                }

                if (value % 0.25M != 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(EntryFee));
                }

                _entryFee = value;
            }
        }

        public float PayoutRatio
        {
            get
            {
                return _payoutRatio;
            }
            private set
            {
                if (value < MinPayoutRatio)
                {
                    throw new ArgumentOutOfRangeException(nameof(PayoutRatio));
                }

                if (value > MaxPayoutRatio)
                {
                    throw new ArgumentOutOfRangeException(nameof(PayoutRatio));
                }

                if ((decimal) value % 0.05M != 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(PayoutRatio));
                }

                _payoutRatio = value;
            }
        }

        public float ServiceChargeRatio
        {
            get
            {
                return _serviceChargeRatio;
            }
            private set
            {
                if (value < MinServiceChargeRatio)
                {
                    throw new ArgumentOutOfRangeException(nameof(ServiceChargeRatio));
                }

                if (value > MaxServiceChargeRatio)
                {
                    throw new ArgumentOutOfRangeException(nameof(ServiceChargeRatio));
                }

                if ((decimal) value % 0.01M != 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(ServiceChargeRatio));
                }

                _serviceChargeRatio = value;
            }
        }

        public bool Generated
        {
            get
            {
                return _generated;
            }
        }

        public int PayoutEntries
        {
            get
            {
                return _helper.PayoutEntries(_entries, _payoutRatio);
            }
        }

        public decimal PrizePool
        {
            get
            {
                return _helper.PrizePool(_entries, _entryFee, _serviceChargeRatio);
            }
        }
    }
}