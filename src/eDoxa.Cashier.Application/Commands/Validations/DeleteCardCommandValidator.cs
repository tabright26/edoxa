// Filename: DeleteCardCommandValidator.cs
// Date Created: 2019-04-21
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
    internal sealed class DeleteCardCommandValidator : CommandValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator()
        {
            this.RuleFor(command => command.CardId).NotEmpty();
        }
    }
}