﻿// Filename: AccountWithdrawalPostRequestValidator.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Domain.AggregateModels;

using FluentValidation;

namespace eDoxa.Cashier.Api.Areas.Accounts.Validators
{
    public sealed class AccountWithdrawalPostRequestValidator : AbstractValidator<AccountWithdrawalPostRequest>
    {
        public AccountWithdrawalPostRequestValidator()
        {
            var amounts = new[] {Money.Fifty, Money.OneHundred, Money.TwoHundred};

            this.RuleFor(request => request.Amount)
                .Must(amount => amounts.Any(money => money.Amount == amount))
                .WithMessage(
                    $"The amount of {nameof(Money)} is invalid. These are valid amounts: [{string.Join(", ", amounts.Select(amount => amount.Amount))}].");
        }
    }
}
