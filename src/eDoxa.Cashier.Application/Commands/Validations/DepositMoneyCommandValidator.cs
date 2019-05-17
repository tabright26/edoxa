// Filename: DepositMoneyCommandValidator.cs
// Date Created: 2019-05-13
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
    internal sealed class DepositMoneyCommandValidator : CommandValidator<DepositMoneyCommand>
    {
        private static readonly MoneyDepositBundles Bundles = new MoneyDepositBundles();

        public DepositMoneyCommandValidator()
        {
            this.RuleFor(command => command.BundleType).Must(Bundles.IsValid).WithMessage("The bundle type doesn't exists.");
        }
    }
}