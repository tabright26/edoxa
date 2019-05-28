// Filename: RemoveBankAccountValidator.cs
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
    public class RemoveBankAccountValidator : AbstractValidator<User>
    {
        public RemoveBankAccountValidator()
        {
            this.RuleFor(account => account)
                .Must(new HasBankAccountSpecification().IsSatisfiedBy)
                .WithMessage("No bank account is associated with this account.");
        }
    }
}
