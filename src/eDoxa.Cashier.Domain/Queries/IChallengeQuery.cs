// Filename: IChallengeQuery.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IChallengeQuery
    {
        IMapper Mapper { get; }

        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);
    }
}
