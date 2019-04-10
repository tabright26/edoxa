// Filename: AsyncCommandHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Handlers
{
    public abstract class AsyncCommandHandler<TCommand> : AsyncRequestHandler<TCommand>, ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        public Task HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            return this.Handle(command, cancellationToken);
        }
    }
}