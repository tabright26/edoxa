// Filename: Payout.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Payout : ValueObject, IPayout
    {
        public Payout(IBuckets buckets)
        {
            Buckets = buckets;
        }

        public IBuckets Buckets { get; }

        public PayoutEntries Entries => new PayoutEntries(Buckets);

        public PrizePool PrizePool => new PrizePool(Buckets);

        public override string ToString()
        {
            return Buckets.ToString()!;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Buckets;
            yield return Entries;
            yield return PrizePool;
        }

        //private IBuckets IndividualBuckets => new Buckets(_buckets.SelectMany(bucket => bucket.AsIndividualBuckets()).OrderByDescending(bucket => bucket.Prize));

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
    }
}
