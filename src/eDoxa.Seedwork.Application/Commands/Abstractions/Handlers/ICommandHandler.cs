// Filename: ICommandHandler.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Abstractions.Handlers
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
    {
    }
}