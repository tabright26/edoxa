// Filename: IUnitOfWork.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace eDoxa.Seedwork.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task CommitAndDispatchDomainEventsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}