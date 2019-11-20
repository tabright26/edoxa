// Filename: IAuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Games.Abstractions.Adapter
{
    public interface IAuthenticationGeneratorAdapter<in TRequest> : IAuthenticationGeneratorAdapter
    where TRequest : class
    {
        Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, TRequest request);
    }

    public interface IAuthenticationGeneratorAdapter
    {
        Game Game { get; }

        Task<ValidationResult> GenerateAuthenticationAsync(UserId userId, object request);
    }
}
