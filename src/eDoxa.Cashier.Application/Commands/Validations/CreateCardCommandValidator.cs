// Filename: CreateCardCommandValidator.cs
// Date Created: 2019-05-13
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
    internal sealed class CreateCardCommandValidator : CommandValidator<CreateBankAccountCommand>
    {
        public CreateCardCommandValidator()
        {
            this.RuleFor(command => command.ExternalAccountTokenId)
                .Must(sourceToken => !string.IsNullOrWhiteSpace(sourceToken))
                .WithMessage("The source token is invalid.");
        }
    }
}
