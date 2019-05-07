﻿// Filename: ChallengeSetup.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public class ChallengeSetup : ValueObject
    {
        private BestOf _bestOf;
        private Entries _entries;
        private EntryFee _entryFee;
        private PayoutRatio _payoutRatio;
        private ServiceChargeRatio _serviceChargeRatio;
        private ChallengeType _type;

        public ChallengeSetup(BestOf bestOf, Entries entries, EntryFee entryFee, PayoutRatio payoutRatio, ServiceChargeRatio serviceChargeRatio, ChallengeType type)
        {
            if (!Enum.IsDefined(typeof(ChallengeType), type))
            {
                throw new InvalidEnumArgumentException(nameof(Type), (int)type, typeof(ChallengeType));
            }

            if (type == ChallengeType.None || type == ChallengeType.All)
            {
                throw new ArgumentException(nameof(Type));
            }

            _bestOf = bestOf;
            _entries = entries;
            _entryFee = entryFee;
            _payoutRatio = payoutRatio;
            _serviceChargeRatio = serviceChargeRatio;
            _type = type;
        }

        public ChallengeType Type => _type;

        public BestOf BestOf => _bestOf;

        public Entries Entries => _entries;

        public EntryFee EntryFee => _entryFee;

        public PayoutRatio PayoutRatio => _payoutRatio;

        public ServiceChargeRatio ServiceChargeRatio => _serviceChargeRatio;

        public PayoutEntries PayoutEntries => new PayoutEntries(_entries, _payoutRatio);

        public PrizePool PrizePool => new PrizePool(_entries, _entryFee, _serviceChargeRatio);
    }
}