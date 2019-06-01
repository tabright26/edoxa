// Filename: AsyncCommandHandler.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Abstractions.Handlers
{
    public abstract class AsyncCommandHandler<TCommand> : AsyncRequestHandler<TCommand>, ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        [NotNull]
        protected abstract override Task Handle([NotNull] TCommand request, CancellationToken cancellationToken);
    }
}