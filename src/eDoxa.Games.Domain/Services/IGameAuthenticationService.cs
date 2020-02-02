// Filename: IAuthFactorService.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Services
{
    public interface IGameAuthenticationService
    {
        Task<GameAuthentication> FindAuthenticationAsync(UserId userId, Game game);

        Task<GameAuthentication<TAuthenticationFactor>> FindAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor :class, IGameAuthenticationFactor;

        Task<bool> AuthenticationExistsAsync(UserId userId, Game game);

        Task<DomainValidationResult<object>> GenerateAuthenticationAsync(UserId userId, Game game, object request);

        Task<DomainValidationResult<object>> ValidateAuthenticationAsync(UserId userId, Game game, GameAuthentication gameAuthentication);
    }
}
