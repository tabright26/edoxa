// Filename: CommandLoggingBehavior.cs
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
using JetBrains.Annotations;
using MediatR;

using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application.Commands.Behaviors
{
    public sealed class CommandLoggingBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : IBaseCommand
    {
        private readonly ILogger<CommandLoggingBehavior<TCommand, TResponse>> _logger;

        public CommandLoggingBehavior(ILogger<CommandLoggingBehavior<TCommand, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [ItemCanBeNull]
        public async Task<TResponse> Handle([NotNull] TCommand command, CancellationToken cancellationToken, [NotNull] RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TCommand).Name}");

            var response = await next();

            _logger.LogInformation($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }
}