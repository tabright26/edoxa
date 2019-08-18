// Filename: IUnitOfWork.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace eDoxa.Seedwork.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IMediator Mediator { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task CommitAndDispatchDomainEventsAsync(CancellationToken cancellationToken = default);
    }
}
