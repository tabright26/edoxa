// Filename: DepositTokenCommandValidator.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    internal sealed class DepositTokenCommandValidator : CommandValidator<DepositTokenCommand>
    {
        private static readonly TokenDepositBundles Bundles = new TokenDepositBundles();

        public DepositTokenCommandValidator()
        {
            this.RuleFor(command => command.BundleType).Must(Bundles.IsValid).WithMessage("The bundle type doesn't exists.");
        }
    }
}