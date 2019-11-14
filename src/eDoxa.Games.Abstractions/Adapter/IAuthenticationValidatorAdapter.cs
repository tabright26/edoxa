// Filename: IAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Abstractions.Adapter
{
    public interface IAuthenticationValidatorAdapter<in TAuthentication> : IAuthenticationValidatorAdapter
    where TAuthentication : Authentication
    {
        Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);
    }

    public interface IAuthenticationValidatorAdapter
    {
        Game Game { get; }

        Task<ValidationResult> ValidateAuthenticationAsync(UserId userId, Authentication authentication);
    }
}
