// Filename: IChallengeQuery.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IChallengeQuery
    {
        Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync();

        Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId);
    }
}
