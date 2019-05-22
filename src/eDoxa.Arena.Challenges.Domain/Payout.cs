﻿// Filename: Payout.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class Payout : ValueObject, IPayout
    {
        private readonly IBuckets _buckets;

        public Payout(IBuckets buckets)
        {
            _buckets = buckets;
        }

        private IBuckets BucketItems => new Buckets(_buckets.SelectMany(bucket => bucket.Items).OrderByDescending(bucket => bucket.Prize));

        public IBuckets Buckets => _buckets;

        public IPayout ApplyFactor(EntryFeeType factor)
        {
            return new Payout(_buckets.ApplyFactor(factor));
        }

        public IParticipantPrizes GetParticipantPrizes(IScoreboard scoreboard)
        {
            var participantPrizes = new ParticipantPrizes();

            for (var index = 0; index < scoreboard.Count; index++)
            {
                var userId = scoreboard.GetUserId(index);

                var prize = this.DetermineParticipantPrize(scoreboard, index);

                participantPrizes.Add(userId, prize);
            }

            return participantPrizes;
        }

        [CanBeNull]
        private Prize DetermineParticipantPrize(IScoreboard scoreboard, int index)
        {
            return scoreboard.IsValidScore(index) ? BucketItems.GetPrize(index) : null;
        }
    }
}
