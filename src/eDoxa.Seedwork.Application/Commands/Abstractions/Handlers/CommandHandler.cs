// Filename: CommandHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Abstractions.Handlers
{
    public abstract class CommandHandler<TCommand, TResponse> : RequestHandler<TCommand, TResponse>, ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    {
    }

    public abstract class CommandHandler<TCommand> : RequestHandler<TCommand>, ICommandHandler<TCommand>
    where TCommand : ICommand
    {
    }
}
