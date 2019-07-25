// Filename: MediatorExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<T> SendAsync<T>(this IMediator mediator, IRequest<T> request)
        {
            return await mediator.Send(request);
        }

        public static async Task SendAsync(this IMediator mediator, IRequest request)
        {
            await mediator.Send(request);
        }
    }

    public static class RequestHandlerExtensions
    {
        public static async Task<TResponse> HandleAsync<TRequest, TResponse>(
            this IRequestHandler<TRequest, TResponse> handler,
            TRequest request,
            CancellationToken cancellationToken = default
        )
        where TRequest : IRequest<TResponse>
        {
            return await handler.Handle(request, cancellationToken);
        }

        public static async Task HandleAsync<TRequest>(
            this IRequestHandler<TRequest, Unit> handler,
            TRequest request,
            CancellationToken cancellationToken = default
        )
        where TRequest : IRequest
        {
            await handler.Handle(request, cancellationToken);
        }
    }
}
