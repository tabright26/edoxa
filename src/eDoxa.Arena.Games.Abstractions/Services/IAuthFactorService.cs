// Filename: IAuthFactorService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.Abstractions.Services
{
    public interface IAuthFactorService
    {
        Task<AuthFactor> FindAuthFactorAsync(UserId userId, Game game);

        Task<bool> AuthFactorExistsAsync(UserId userId, Game game);

        Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, Game game, object request);

        Task<ValidationResult> ValidateAuthFactorAsync(UserId userId, Game game, AuthFactor authFactor);
    }
}
