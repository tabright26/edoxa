// Filename: CreateBankAccountCommandValidator.cs
// Date Created: 2019-05-10
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
    internal sealed class CreateBankAccountCommandValidator : CommandValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountCommandValidator()
        {
            this.RuleFor(command => command.SourceToken).Must(sourceToken => !string.IsNullOrWhiteSpace(sourceToken));
        }
    }
}