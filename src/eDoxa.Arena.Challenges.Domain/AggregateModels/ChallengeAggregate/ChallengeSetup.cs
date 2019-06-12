// Filename: ChallengeSetup.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeSetup : ValueObject
    {
        public ChallengeSetup(BestOf bestOf, PayoutEntries payoutEntries, EntryFee entryFee) : this()
        {
            BestOf = bestOf;
            Entries = new Entries(payoutEntries);
            PayoutEntries = payoutEntries;
            EntryFee = new EntryFee(entryFee.Type, entryFee.Amount); // Required by EF Core.
            PrizePool = new PrizePool(Entries, EntryFee);
        }

        private ChallengeSetup()
        {
            // Required by EF Core.
        }

        public BestOf BestOf { get; private set; }

        public Entries Entries { get; private set; }

        public EntryFee EntryFee { get; private set; }

        public PrizePool PrizePool { get; private set; }

        public PayoutEntries PayoutEntries { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BestOf;
            yield return Entries;
            yield return EntryFee;
            yield return PayoutEntries;
            yield return PrizePool;
        }

        public override string ToString()
        {
            return string.Join(",", this.GetAtomicValues().Select(signature => $"{signature.GetType().Name}={signature.ToString()}"));
        }
    }
}
