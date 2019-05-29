﻿// Filename: UpdateCardDefaultCommandValidator.cs
// Date Created: 2019-05-06
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
    public sealed class UpdateCardDefaultCommandValidator : CommandValidator<UpdateCardDefaultCommand>
    {
        public UpdateCardDefaultCommandValidator()
        {
            this.RuleFor(command => command.StripeCardId).NotNull().WithMessage("The CardId is invalid.");
        }
    }
}
