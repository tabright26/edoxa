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
        public Challenge(ChallengeId challengeId, EntryFee entryFee, IChallengePayout payout)
        {
            this.SetEntityId(challengeId);
            EntryFee = entryFee;
            Payout = payout;
        }

        public EntryFee EntryFee { get; }

        public IChallengePayout Payout { get; }

        public void Close(ChallengeScoreboard scoreboard)
        {
            var payoutPrizes = new PayoutPrizes();

            foreach (var ladderGroup in scoreboard.Ladders)
            {
                for (var index = 0; index < ladderGroup.Size; index++)
                {
                    var userId = scoreboard.Winners.Dequeue();

                    var score = scoreboard[userId];

                    if (score == null)
                    {
                        var currency = scoreboard.PayoutCurrency.From(0);

                        this.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(userId, currency));

                        payoutPrizes.Add(userId, currency);
                    }
                    else
                    {
                        var currency = scoreboard.PayoutCurrency.From(ladderGroup.Prize);

                        this.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(userId, currency));

                        payoutPrizes.Add(userId, currency);
                    }
                }
            }

            foreach (var user in scoreboard.Losers)
            {
                var score = scoreboard[user];

                if (score == null)
                {
                    var currency = scoreboard.PayoutCurrency.From(0);

                    this.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(user, currency));

                    payoutPrizes.Add(user, currency);
                }
                else
                {
                    var currency = Currency.Token.From(Token.MinValue);

                    this.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(user, currency));

                    payoutPrizes.Add(user, currency);
                }
            }

            this.AddDomainEvent(new ChallengeClosedDomainEvent(Id, payoutPrizes));
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
