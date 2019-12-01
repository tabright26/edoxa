// Filename: IAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Abstractions.Adapter
{
    public interface IAuthenticationValidatorAdapter<in TAuthentication> : IAuthenticationValidatorAdapter
    where TAuthentication : GameAuthentication
    {
        Task<DomainValidationResult> ValidateAuthenticationAsync(UserId userId, TAuthentication authentication);
    }

    public interface IAuthenticationValidatorAdapter
    {
        Game Game { get; }

        Task<DomainValidationResult> ValidateAuthenticationAsync(UserId userId, GameAuthentication gameAuthentication);
    }
}
