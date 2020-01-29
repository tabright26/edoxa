// Filename: IChallenge.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallenge : IEntity<ChallengeId>, IAggregateRoot
    {
        EntryFee EntryFee { get; }

        IChallengePayout Payout { get; }
    }
}
