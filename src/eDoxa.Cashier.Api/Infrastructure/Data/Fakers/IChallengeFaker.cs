// Filename: IChallengeFaker.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers
{
    public interface IChallengeFaker
    {
        IReadOnlyCollection<IChallenge> FakeChallenges(int count);

        IChallenge FakeChallenge();
    }
}
