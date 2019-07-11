// Filename: FakeChallengesCommandValidator.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Validations
{
    public sealed class FakeChallengesCommandValidator : CommandValidator<FakeChallengesCommand>
    {
        public FakeChallengesCommandValidator()
        {
            this.RuleFor(command => command.Count).InclusiveBetween(1, 10);

            this.RuleFor(command => command.Seed).InclusiveBetween(1, int.MaxValue);

            this.OptionalEnumeration(command => command.Game);

            this.OptionalEnumeration(command => command.State);

            //this.OptionalEnumeration(command => command.EntryFeeCurrency);
        }
    }
}
