// Filename: IArenaChallengeTestFileStorage.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public interface IArenaChallengeTestFileStorage
    {
        Task<IImmutableSet<User>> GetUsersAsync();

        Task<IImmutableSet<IChallenge>> GetChallengesAsync();
    }
}
