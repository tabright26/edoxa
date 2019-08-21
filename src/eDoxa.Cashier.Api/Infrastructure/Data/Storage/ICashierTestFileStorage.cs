// Filename: ICashierTestFileStorage.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public interface ICashierTestFileStorage
    {
        Task<IImmutableSet<User>> GetUsersAsync();

        Task<IImmutableSet<IChallenge>> GetChallengesAsync();
    }
}
