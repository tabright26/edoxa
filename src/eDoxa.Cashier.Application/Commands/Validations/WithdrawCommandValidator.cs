// Filename: WithdrawCommandValidator.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    public sealed class WithdrawCommandValidator : CommandValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            var amounts = new[] {Money.Fifty, Money.OneHundred, Money.TwoHundred};

            this.RuleFor(command => command.Amount)
                .Must(amount => amounts.Any(money => money.Amount == amount))
                .WithMessage(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}]."
                );
        }
    }
}
