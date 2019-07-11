// Filename: IChallenge.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallenge : IEntity<ChallengeId>, IAggregateRoot
    {
        EntryFee EntryFee { get; }

        IPayout Payout { get; }
    }
}
