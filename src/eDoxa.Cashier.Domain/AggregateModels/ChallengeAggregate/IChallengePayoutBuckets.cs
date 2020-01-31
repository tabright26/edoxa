// Filename: IChallengePayoutBuckets.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallengePayoutBuckets : IReadOnlyList<ChallengePayoutBucket>
    {
    }
}
