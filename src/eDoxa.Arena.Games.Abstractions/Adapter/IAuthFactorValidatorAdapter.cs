// Filename: IAuthFactorValidatorAdapter.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.Abstractions.Adapter
{
    public interface IAuthFactorValidatorAdapter
    {
        Game Game { get; }

        Task<ValidationResult> ValidateAuthFactorAsync(UserId userId, AuthFactor authFactor);
    }
}
