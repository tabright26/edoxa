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

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeSetup : ValueObject
    {
        public ChallengeSetup(BestOf bestOf, PayoutEntries payoutEntries, EntryFee entryFee)
        {
            BestOf = bestOf;
            Entries = new Entries(payoutEntries);
            PayoutEntries = payoutEntries;
            EntryFee = entryFee;
        }

        public BestOf BestOf { get; }

        public Entries Entries { get; }

        public EntryFee EntryFee { get; }

        public PayoutEntries PayoutEntries { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BestOf;
            yield return Entries;
            yield return EntryFee;
            yield return PayoutEntries;
        }

        public override string ToString()
        {
            return string.Join(",", this.GetAtomicValues().Select(signature => $"{signature.GetType().Name}={signature.ToString()}"));
        }
    }
}
