// Filename: IAuthFactorService.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<GameAuthentication> FindAuthenticationAsync(UserId userId, Game game);

        Task<GameAuthentication<TAuthenticationFactor>> FindAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor :class, IGameAuthenticationFactor;

        Task<bool> AuthenticationExistsAsync(UserId userId, Game game);

        Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, Game game, object request);

        Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, Game game, GameAuthentication gameAuthentication);
    }
}
