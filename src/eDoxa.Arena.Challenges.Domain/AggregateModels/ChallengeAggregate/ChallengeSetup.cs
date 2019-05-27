// Filename: ChallengeSetup.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class ChallengeSetup : ValueObject
    {
        private BestOf _bestOf;
        private Entries _entries;
        private EntryFee _entryFee;
        private PayoutRatio _payoutRatio;
        private ServiceChargeRatio _serviceChargeRatio;

        public ChallengeSetup(BestOf bestOf, PayoutEntries payoutEntries, EntryFee entryFee) : this()
        {
            _bestOf = bestOf;
            _entryFee = entryFee;
            _entries = new Entries(payoutEntries, _payoutRatio);
        }

        private ChallengeSetup()
        {
            _payoutRatio = PayoutRatio.Default;
            _serviceChargeRatio = ServiceChargeRatio.Default;
        }

        public BestOf BestOf => _bestOf;

        public Entries Entries => _entries;

        public EntryFee EntryFee => _entryFee;

        public PayoutRatio PayoutRatio => _payoutRatio;

        public ServiceChargeRatio ServiceChargeRatio => _serviceChargeRatio;

        public PayoutEntries PayoutEntries => new PayoutEntries(_entries, _payoutRatio);

        public PrizePool PrizePool => new PrizePool(_entries, _entryFee, _serviceChargeRatio);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BestOf;
            yield return Entries;
            yield return EntryFee;
            yield return PayoutRatio;
            yield return ServiceChargeRatio;
            yield return PayoutEntries;
            yield return PrizePool;
        }

        public override string ToString()
        {
            return string.Join(", ", this.GetAtomicValues());
        }
    }
}
