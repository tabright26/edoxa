// Filename: Challenge.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

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
