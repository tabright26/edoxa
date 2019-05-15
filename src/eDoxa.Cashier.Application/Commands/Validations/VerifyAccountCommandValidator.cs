// Filename: VerifyBankAccountCommandValidator.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    internal sealed class VerifyAccountCommandValidator : CommandValidator<VerifyAccountCommand>
    {
        public VerifyAccountCommandValidator()
        {
            this.RuleFor(command => command.Line1).NotNull();
            this.RuleFor(command => command.City).NotNull();
            this.RuleFor(command => command.State).NotNull();
            this.RuleFor(command => command.PostalCode).NotNull();
            this.RuleFor(command => command.TermsOfService).Must(termsOfService => termsOfService);
        }
    }
}