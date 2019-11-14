// Filename: IAuthFactorService.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<Authentication> FindAuthenticationAsync(UserId userId, Game game);

        Task<Authentication<TAuthenticationFactor>> FindAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor :class, IAuthenticationFactor;

        Task<bool> AuthenticationExistsAsync(UserId userId, Game game);

        Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, Game game, object request);

        Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, Game game, Authentication authentication);
    }
}
