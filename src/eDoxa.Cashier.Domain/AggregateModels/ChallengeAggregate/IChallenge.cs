// Filename: IChallenge.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public interface IChallenge : IEntity<ChallengeId>, IAggregateRoot
    {
        IPayout Payout { get; }
    }
}
