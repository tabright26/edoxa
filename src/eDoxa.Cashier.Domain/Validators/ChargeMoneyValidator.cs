// Filename: ChargeMoneyValidator.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Specifications;

using FluentValidation;

namespace eDoxa.Cashier.Domain.Validators
{
    public sealed class ChargeMoneyValidator : AbstractValidator<MoneyAccount>
    {
        public ChargeMoneyValidator(Money money)
        {
            this.RuleFor(account => account).Must(new InsufficientMoneySpecification(money).Not().IsSatisfiedBy).WithMessage("Insufficient funds.");
        }
    }
}
