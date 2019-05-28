// Filename: AddBankAccountValidator.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Specifications;

using FluentValidation;

namespace eDoxa.Cashier.Domain.Validators
{
    public class AddBankAccountValidator : AbstractValidator<User>
    {
        public AddBankAccountValidator()
        {
            this.RuleFor(account => account)
                .Must(new HasBankAccountSpecification().Not().IsSatisfiedBy)
                .WithMessage("A bank account is already associated with this account.");
        }
    }
}
