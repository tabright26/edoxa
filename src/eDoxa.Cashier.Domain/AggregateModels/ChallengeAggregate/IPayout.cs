// Filename: IPayout.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IPayout
    {
        PayoutEntries Entries { get; }

        PrizePool PrizePool { get; }

        IBuckets Buckets { get; }
    }
}
