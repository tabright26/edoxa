// Filename: WithdrawMoneyValidator.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Specifications;

using FluentValidation;

namespace eDoxa.Cashier.Domain.Validators
{
    public class WithdrawMoneyValidator : AbstractValidator<MoneyAccount>
    {
        public WithdrawMoneyValidator(Money money)
        {
            this.RuleFor(account => account.User).Must(new HasBankAccountSpecification().IsSatisfiedBy).WithMessage("A bank account is required to withdrawal.");

            this.RuleFor(account => account).Must(new InsufficientMoneySpecification(money).Not().IsSatisfiedBy).WithMessage("Insufficient funds.");

            this.RuleFor(account => account)
                .Must(new WeeklyMoneyWithdrawUnavailableSpecification().Not().IsSatisfiedBy)
                .WithMessage(account => $"Withdraw unavailable until {account.LastWithdraw?.AddDays(7)}");
        }
    }
}
