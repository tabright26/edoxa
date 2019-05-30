// Filename: ChargeTokenValidator.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Specifications;

using FluentValidation;

namespace eDoxa.Cashier.Domain.Validators
{
    public sealed class ChargeTokenValidator : AbstractValidator<TokenAccount>
    {
        public ChargeTokenValidator(Token token)
        {
            this.RuleFor(account => account).Must(new InsufficientTokenSpecification(token).Not().IsSatisfiedBy).WithMessage("Insufficient funds.");
        }
    }
}
