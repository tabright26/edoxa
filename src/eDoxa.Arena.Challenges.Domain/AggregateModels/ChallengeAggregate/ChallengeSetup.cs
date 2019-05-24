// Filename: ChallengeSetup.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class ChallengeSetup : ValueObject
    {
        private BestOf _bestOf;
        private Entries _entries;
        private EntryFee _entryFee;
        private Currency _entryFeeCurrency;
        private PayoutRatio _payoutRatio;
        private Currency _payoutCurrency;
        private ServiceChargeRatio _serviceChargeRatio;

        public ChallengeSetup(BestOf bestOf, Entries entries, EntryFee entryFee, Currency payoutCurrency) : this()
        {
            _bestOf = bestOf;
            _entries = entries;
            _entryFee = entryFee;
            _entryFeeCurrency = entryFee is MoneyEntryFee ? Currency.Money : entryFee is TokenEntryFee ? Currency.Token : throw new ArgumentException(nameof(entryFee));
            _payoutCurrency = payoutCurrency;
        }

        private ChallengeSetup()
        {
            _payoutRatio = PayoutRatio.Default;
            _serviceChargeRatio = ServiceChargeRatio.Default;
        }

        public BestOf BestOf => _bestOf;

        public Entries Entries => _entries;

        public EntryFee EntryFee => _entryFee;

        public Currency EntryFeeCurrency => _entryFeeCurrency;

        public PayoutRatio PayoutRatio => _payoutRatio;

        public Currency PayoutCurrency => _payoutCurrency;

        public ServiceChargeRatio ServiceChargeRatio => _serviceChargeRatio;

        public PayoutEntries PayoutEntries => new PayoutEntries(_entries, _payoutRatio);

        public PrizePool PrizePool => new PrizePool(_entries, _entryFee, _serviceChargeRatio);
    }
}