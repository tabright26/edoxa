// Filename: Challenge.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Challenge : Entity<ChallengeId>, IChallenge
    {
        public Challenge(IPayout payout)
        {
            Payout = payout;
        }

        public IPayout Payout { get; }
    }

    public partial class Challenge : IEquatable<IChallenge>
    {
        public bool Equals([CanBeNull] IChallenge challenge)
        {
            return Id.Equals(challenge?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as IChallenge);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
