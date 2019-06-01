// Filename: CommandLoggingBehavior.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Commands.Abstractions;

using JetBrains.Annotations;

using MediatR;

using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application.Commands.Behaviors
{
    public sealed class CommandLoggingBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : IBaseCommand
    {
        private readonly ILogger<CommandLoggingBehavior<TCommand, TResult>> _logger;

        public CommandLoggingBehavior(ILogger<CommandLoggingBehavior<TCommand, TResult>> logger)
        {
            _logger = logger;
        }

        [ItemNotNull]
        public async Task<TResult> Handle([NotNull] TCommand command, CancellationToken cancellationToken, [NotNull] RequestHandlerDelegate<TResult> next)
        {
            _logger.LogInformation($"Handling {typeof(TCommand).Name}...");

            var result = await next();

            _logger.LogInformation($"Handled {typeof(TResult).Name}.");

            return result;
        }
    }
}