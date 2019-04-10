// Filename: CommandHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Handlers
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