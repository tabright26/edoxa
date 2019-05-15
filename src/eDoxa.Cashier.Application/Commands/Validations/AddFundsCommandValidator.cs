// Filename: AddFundsCommandValidator.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    internal sealed class AddFundsCommandValidator : CommandValidator<AddFundsCommand>
    {
        private static readonly MoneyBundles Bundles = new MoneyBundles();

        public AddFundsCommandValidator()
        {
            this.RuleFor(command => command.BundleType).Must(Bundles.IsValid).WithMessage("The bundle type doesn't exists.");
        }
    }
}