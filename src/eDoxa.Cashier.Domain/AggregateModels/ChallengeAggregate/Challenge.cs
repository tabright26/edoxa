// Filename: Challenge.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Challenge : Entity<ChallengeId>, IChallenge
    {
        public Challenge(ChallengeId challengeId, IChallengePayout payout)
        {
            this.SetEntityId(challengeId);
            Payout = payout;
        }

        public IChallengePayout Payout { get; }

        public void Close(ChallengeScoreboard scoreboard)
        {
            var payouts = new ChallengeParticipantPayouts();

            foreach (var bucket in scoreboard.Buckets)
            {
                for (var index = 0; index < bucket.Size; index++)
                {
                    var userId = scoreboard.Winners.Dequeue();

                    var score = scoreboard[userId];

                    var currency = score == null ? scoreboard.PayoutCurrencyType.ToCurrency(0) : bucket.Prize;

                    this.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(userId, currency));

                    payouts.Add(userId, new ChallengePayoutBucketPrize(currency));
                }
            }

            foreach (var userId in scoreboard.Losers)
            {
                var score = scoreboard[userId];

                var currency = score == null ? scoreboard.PayoutCurrencyType.ToCurrency(0) : ChallengePayoutBucketPrize.Consolation;

                this.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(userId, currency));

                payouts.Add(userId, new ChallengePayoutBucketPrize(currency));
            }

            this.AddDomainEvent(new ChallengeClosedDomainEvent(Id, payouts));
        }
    }

    public partial class Challenge : IEquatable<IChallenge?>
    {
        public bool Equals(IChallenge? challenge)
        {
            return Id.Equals(challenge?.Id);
        }

        public sealed override bool Equals(object? obj)
        {
            return this.Equals(obj as IChallenge);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
