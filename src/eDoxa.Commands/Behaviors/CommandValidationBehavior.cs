// Filename: CommandValidationBehavior.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using JetBrains.Annotations;

using MediatR;

using Microsoft.Extensions.Logging;

namespace eDoxa.Commands.Behaviors
{
    public sealed class CommandValidationBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    {
        private readonly ILogger<CommandValidationBehavior<TCommand, TResult>> _logger;
        private readonly IValidator<TCommand>[] _validators;

        public CommandValidationBehavior(IValidator<TCommand>[] validators, ILogger<CommandValidationBehavior<TCommand, TResult>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        [ItemNotNull]
        public async Task<TResult> Handle([NotNull] TCommand command, CancellationToken cancellationToken, [NotNull] RequestHandlerDelegate<TResult> next)
        {
            _logger.LogInformation($"Validating {typeof(TCommand).Name}...");

            var errors = _validators.Select(validator => validator.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (!errors.Any())
            {
                return await next();
            }

            _logger.LogWarning($"Validation failed for {typeof(TCommand).Name}.");

            throw new ValidationException(errors);
        }
    }
}
