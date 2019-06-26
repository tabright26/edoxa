// Filename: IndividualBucket.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class IndividualBucket : Bucket
    {
        internal IndividualBucket(Prize prize) : base(prize, BucketSize.Individual)
        {
        }
    }
}
