// Filename: IAuthFactorGeneratorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Abstractions.Adapter
{
    public interface IAuthenticationGeneratorAdapter<in TRequest> : IAuthenticationGeneratorAdapter
    where TRequest : class
    {
        Task<DomainValidationResult> GenerateAuthenticationAsync(UserId userId, TRequest request);
    }

    public interface IAuthenticationGeneratorAdapter
    {
        Game Game { get; }

        Task<DomainValidationResult> GenerateAuthenticationAsync(UserId userId, object request);
    }
}
