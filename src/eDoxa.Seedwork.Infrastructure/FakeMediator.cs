// Filename: NoMediator.cs
// Date Created: 2019-06-20
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

namespace eDoxa.Seedwork.Infrastructure
{
    public sealed class FakeMediator : IMediator
    {
        [NotNull]
        public Task Publish([NotNull] object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        [NotNull]
        public Task Publish<TNotification>([NotNull] TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        [ItemNotNull]
        public async Task<TResponse> Send<TResponse>([NotNull] IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(default(TResponse));
        }
    }
}
