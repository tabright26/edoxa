// Filename: IAuthenticationRepository.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Games.Domain.Repositories
{
    public interface IAuthenticationRepository
    {
        Task AddAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game, Authentication<TAuthenticationFactor> authentication)
        where TAuthenticationFactor : class, IAuthenticationFactor;

        Task RemoveAuthenticationAsync(UserId userId, Game game);

        Task<Authentication<TAuthenticationFactor>> GetAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor : class, IAuthenticationFactor;

        Task<Authentication> GetAuthenticationAsync(UserId userId, Game game);

        Task<bool> AuthenticationExistsAsync(UserId userId, Game game);
    }
}
