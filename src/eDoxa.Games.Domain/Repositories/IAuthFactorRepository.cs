// Filename: IAuthFactorRepository.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Games.Domain.Repositories
{
    public interface IAuthFactorRepository
    {
        Task AddAuthFactorAsync(UserId userId, Game game, AuthFactor authFactor);

        Task RemoveAuthFactorAsync(UserId userId, Game game);

        Task<AuthFactor> GetAuthFactorAsync(UserId userId, Game game);

        Task<bool> AuthFactorExistsAsync(UserId userId, Game game);
    }
}
