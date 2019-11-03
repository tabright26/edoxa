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
    public interface IAuthFactorGeneratorAdapter
    {
        Game Game { get; }

        Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, object request);
    }

    public interface IAuthFactorGeneratorAdapter<in TRequest> : IAuthFactorGeneratorAdapter
    where TRequest : class
    {
        Task<ValidationResult> GenerateAuthFactorAsync(UserId userId, TRequest request);
    }
}
