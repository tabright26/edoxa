﻿// Filename: CommandValidator.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using FluentValidation;

namespace eDoxa.Seedwork.Application.Commands.Abstractions.Validations
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : IBaseCommand
    {
    }
}