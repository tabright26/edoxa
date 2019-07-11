// Filename: IBuckets.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IBuckets : IReadOnlyList<Bucket>
    {
        Prize PrizeAtOrDefault(int index);
    }
}
