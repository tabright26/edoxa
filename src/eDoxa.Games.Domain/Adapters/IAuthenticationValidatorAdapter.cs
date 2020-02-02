// Filename: IAuthenticationValidatorAdapter.cs
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
    public interface IAuthenticationValidatorAdapter<TAuthentication> : IAuthenticationValidatorAdapter
    where TAuthentication : GameAuthentication
    {
        Task<DomainValidationResult<object>> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);
    }

    public interface IAuthenticationValidatorAdapter
    {
        Game Game { get; }

        Task<DomainValidationResult<object>> ValidateAuthenticationAsync(UserId userId, GameAuthentication gameAuthentication);
    }
}
