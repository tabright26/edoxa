// Filename: IAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Adapters
{
    public interface IAuthenticationValidatorAdapter<in TAuthentication> : IAuthenticationValidatorAdapter
    where TAuthentication : GameAuthentication
    {
        Task<IDomainValidationResult> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);
    }

    public interface IAuthenticationValidatorAdapter
    {
        Game Game { get; }

        Task<IDomainValidationResult> ValidateAuthenticationAsync(UserId userId, GameAuthentication gameAuthentication);
    }
}
