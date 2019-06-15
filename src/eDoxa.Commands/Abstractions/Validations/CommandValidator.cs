// Filename: CommandValidator.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using FluentValidation;

namespace eDoxa.Commands.Abstractions.Validations
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : IBaseCommand
    {
    }
}
