// Filename: IndividualBucket.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class IndividualBucket : Bucket
    {
        internal IndividualBucket(Prize prize) : base(prize, BucketSize.Individual)
        {
        }
    }
}
