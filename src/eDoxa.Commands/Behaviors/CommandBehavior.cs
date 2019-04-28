// Filename: CommandBehavior.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Commands.Abstractions;
using eDoxa.Commands.Services;

using JetBrains.Annotations;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Commands.Behaviors
{
    public sealed class CommandBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : IBaseCommand
    {
        private readonly ICommandService _commandService;
        private readonly IServiceProvider _serviceProvider;

        public CommandBehavior(ICommandService commandService, IServiceProvider serviceProvider)
        {
            _commandService = commandService;
            _serviceProvider = serviceProvider;
        }

        [ItemNotNull]
        public async Task<TResult> Handle([NotNull] TCommand command, CancellationToken cancellationToken, [NotNull] RequestHandlerDelegate<TResult> next)
        {
            var result = await next();

            if (!typeof(IActionResult).IsAssignableFrom(typeof(TResult)))
            {
                return result;
            }

            var accessor = _serviceProvider.GetService<IHttpContextAccessor>();

            if (accessor?.HttpContext is HttpContext context)
            {
                await _commandService.LogEntryAsync((ICommand<IActionResult>) command, (IActionResult) result, context);
            }

            return result;
        }
    }
}