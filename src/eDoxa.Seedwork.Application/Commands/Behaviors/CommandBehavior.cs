﻿// Filename: CommandBehavior.cs
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

using eDoxa.Seedwork.Application.Services;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Commands.Behaviors
{
    /// <summary>
    ///     Provides a base implementation for handling duplicate request and ensuring request updates, in the cases where a
    ///     request sent by client is used to detect duplicate requests.
    /// </summary>
    /// <typeparam name="TCommand">The type of the <see cref="ICommand{TResponse}" />.</typeparam>
    /// <typeparam name="TResponse">The type of the command response.</typeparam>
    public sealed class CommandBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : IBaseCommand
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRequestLogService _requestLogService;

        public CommandBehavior(IHttpContextAccessor contextAccessor, IRequestLogService requestLogService)
        {
            _contextAccessor = contextAccessor;
            _requestLogService = requestLogService ?? throw new ArgumentNullException(nameof(requestLogService));
        }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //TODO: This must be implemented before eDoxa v.3 (Release 1)
            //var response = await next();

            var httpContext = _contextAccessor?.HttpContext;

            //TODO: This must be implemented before eDoxa v.3 (Release 1)
            await _requestLogService.CreateAsync(httpContext /*, command, response*/);

            return await next();
        }
    }
}