﻿// Filename: CommandHandlerExtensions.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Commands.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;

using MediatR;

namespace eDoxa.Commands.Extensions
{
    public static class CommandHandlerExtensions
    {
        public static async Task<TResult> HandleAsync<TCommand, TResult>(this ICommandHandler<TCommand, TResult> handler, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResult>
        {
            return await handler.Handle(command, cancellationToken);
        }

        public static async Task HandleAsync<TCommand>(this ICommandHandler<TCommand, Unit> handler, TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<Unit>
        {
            await handler.Handle(command, cancellationToken);
        }
    }
}