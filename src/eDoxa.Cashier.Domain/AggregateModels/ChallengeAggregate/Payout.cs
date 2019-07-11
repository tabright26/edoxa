﻿// Filename: Payout.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Payout : ValueObject, IPayout
    {
        private readonly IBuckets _buckets;

        public Payout(IBuckets buckets)
        {
            _buckets = buckets;
        }

        public PrizePool PrizePool => new PrizePool(_buckets);

        //private IBuckets IndividualBuckets => new Buckets(_buckets.SelectMany(bucket => bucket.AsIndividualBuckets()).OrderByDescending(bucket => bucket.Prize));

        public IBuckets Buckets => _buckets;

        //public IParticipantPrizes GetParticipantPrizes(IScoreboard scoreboard)
        //{
        //    var participantPrizes = new ParticipantPrizes();

        //    for (var index = 0; index < scoreboard.Count; index++)
        //    {
        //        var userId = scoreboard.UserIdAt(index);

        //        var prize = this.DetermineParticipantPrize(scoreboard, index);

        //        participantPrizes.Add(userId, prize);
        //    }

        //    return participantPrizes;
        //}

        //[CanBeNull]
        //private Prize DetermineParticipantPrize(IScoreboard scoreboard, int index)
        //{
        //    return scoreboard.IsValidScore(index) ? IndividualBuckets.PrizeAtOrDefault(index) : null;
        //}

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PrizePool;
            yield return Buckets;
        }

        public override string ToString()
        {
            return Buckets.ToString();
        }
    }
}
