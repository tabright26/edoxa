// Filename: IAuthenticationGeneratorAdapter.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Adapters
{
    public interface IAuthenticationGeneratorAdapter<in TRequest> : IAuthenticationGeneratorAdapter
    where TRequest : class
    {
        Task<DomainValidationResult<object>> GenerateAuthenticationAsync(UserId userId, TRequest request);
    }

    public interface IAuthenticationGeneratorAdapter
    {
        Game Game { get; }

        Task<DomainValidationResult<object>> GenerateAuthenticationAsync(UserId userId, object request);
    }
}
