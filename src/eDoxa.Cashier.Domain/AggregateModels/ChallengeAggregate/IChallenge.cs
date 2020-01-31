// Filename: IChallenge.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallenge : IEntity<ChallengeId>, IAggregateRoot
    {
        IChallengePayout Payout { get; }

        void Close(ChallengeScoreboard scoreboard);
    }
}
