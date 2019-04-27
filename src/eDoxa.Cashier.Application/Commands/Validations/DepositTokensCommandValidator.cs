﻿// Filename: DepositTokensCommandValidator.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Commands.Validations;

using FluentValidation;

namespace eDoxa.Cashier.Application.Commands.Validations
{
    internal sealed class DepositTokensCommandValidator : CommandValidator<DepositTokensCommand>
    {
        public DepositTokensCommandValidator()
        {
            this.RuleFor(command => command.UserId).NotEmpty();
        }
    }
}