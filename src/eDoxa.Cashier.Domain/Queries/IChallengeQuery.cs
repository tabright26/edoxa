// Filename: IChallengeQuery.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IChallengeQuery
    {
        IMapper Mapper { get; }

        Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync();

        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);
    }
}
