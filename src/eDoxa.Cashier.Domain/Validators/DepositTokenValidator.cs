// Filename: DepositTokenValidator.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Specifications;

using FluentValidation;

namespace eDoxa.Cashier.Domain.Validators
{
    public class DepositTokenValidator : AbstractValidator<TokenAccount>
    {
        public DepositTokenValidator()
        {
            this.RuleFor(account => account)
                .Must(new DailyTokenDepositUnavailableSpecification().Not().IsSatisfiedBy)
                .WithMessage(account => $"Deposit unavailable until {account.LastDeposit?.AddDays(1)}");
        }
    }
}
