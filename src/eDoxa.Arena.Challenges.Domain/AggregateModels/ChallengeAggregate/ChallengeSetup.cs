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
        public ChallengeSetup(BestOf bestOf, PayoutEntries payoutEntries, EntryFee entryFee) : this()
        {
            BestOf = bestOf;
            EntryFee = entryFee;
            Entries = new Entries(payoutEntries, PayoutRatio);
        }

        private ChallengeSetup()
        {
            PayoutRatio = PayoutRatio.Default;
            ServiceChargeRatio = ServiceChargeRatio.Default;
        }

        public BestOf BestOf { get; private set; }

        public Entries Entries { get; private set; }

        public EntryFee EntryFee { get; private set; }

        public PayoutRatio PayoutRatio { get; private set; }

        public ServiceChargeRatio ServiceChargeRatio { get; private set; }

        public PayoutEntries PayoutEntries => new PayoutEntries(Entries, PayoutRatio);

        public PrizePool PrizePool => new PrizePool(Entries, EntryFee, ServiceChargeRatio);

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
