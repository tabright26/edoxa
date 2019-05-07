// Filename: Payout.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public sealed class Payout : ValueObject, IPayout
    {
        private readonly List<Bucket> _buckets;

        public Payout()
        {
            _buckets = new List<Bucket>();
        }

        public IReadOnlyList<Bucket> Buckets => _buckets;

        public void AddBucket(Prize prize, int size)
        {
            _buckets.Add(new Bucket(prize, size));
        }
    }
}