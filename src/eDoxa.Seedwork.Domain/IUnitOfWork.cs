// Filename: IUnitOfWork.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace eDoxa.Seedwork.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default);
    }
}
