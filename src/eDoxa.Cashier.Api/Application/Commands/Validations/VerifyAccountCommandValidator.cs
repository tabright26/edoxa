// Filename: VerifyAccountCommandValidator.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Api.Application.Commands.Validations
{
    public sealed class VerifyAccountCommandValidator : CommandValidator<VerifyAccountCommand>
    {
        public VerifyAccountCommandValidator()
        {
            this.RuleFor(command => command.Line1).Must(line1 => !string.IsNullOrWhiteSpace(line1)).WithMessage("The Line1 is invalid.");

            this.RuleFor(command => command.Line2).Must(line2 => line2 == null || !string.IsNullOrWhiteSpace(line2)).WithMessage("The Line2 is invalid.");

            this.RuleFor(command => command.City).Must(city => !string.IsNullOrWhiteSpace(city)).WithMessage("The City is invalid.");

            this.RuleFor(command => command.State).Must(state => !string.IsNullOrWhiteSpace(state)).WithMessage("The State is invalid.");

            this.RuleFor(command => command.PostalCode).Must(postalCode => !string.IsNullOrWhiteSpace(postalCode)).WithMessage("The PostalCode is invalid.");

            this.RuleFor(command => command.TermsOfService).Must(termsOfService => termsOfService).WithMessage("You must agree to Stripe's Terms of Service.");
        }
    }
}
