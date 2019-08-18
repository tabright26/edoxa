// Filename: IRequestLoggingBehavior.cs
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

using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application
{
    public sealed class RequestLoggingBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : IBaseRequest
    {
        private readonly ILogger<RequestLoggingBehavior<TRequest, TResult>> _logger;

        public RequestLoggingBehavior(ILogger<RequestLoggingBehavior<TRequest, TResult>> logger)
        {
            _logger = logger;
        }

        
        public async Task<TResult> Handle( TRequest request, CancellationToken cancellationToken,  RequestHandlerDelegate<TResult> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}...");

            var result = await next();

            _logger.LogInformation($"Handled {typeof(TResult).Name}.");

            return result;
        }
    }
}
