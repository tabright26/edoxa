// Filename: UpdateAddressCommandValidator.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Commands.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    public sealed class UpdateAddressCommandValidator : CommandValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            this.RuleFor(command => command.UserId).NotEmpty();
        }
    }
}