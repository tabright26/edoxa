// Filename: CreateCardCommandValidator.cs
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
    public sealed class CreateCardCommandValidator : CommandValidator<CreateBankAccountCommand>
    {
        public CreateCardCommandValidator()
        {
            this.RuleFor(command => command.ExternalAccountTokenId)
                .Must(sourceToken => !string.IsNullOrWhiteSpace(sourceToken))
                .WithMessage("The source token is invalid.");
        }
    }
}
