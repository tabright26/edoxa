// Filename: StripeCustomerValidator.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using FluentValidation;

using Stripe;

namespace eDoxa.Stripe.Validators
{
    public class StripeCustomerValidator : AbstractValidator<Customer>
    {
        public StripeCustomerValidator()
        {
            this.RuleFor(customer => customer.DefaultSource)
                .NotNull()
                .WithMessage("There are no credit cards associated with this account.")
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(customer => customer.DefaultSource.Object)
                            .Must(defaultSourceType => defaultSourceType == "card")
                            .WithMessage("The default source card is not a credit card.");
                    }
                );
        }
    }
}
