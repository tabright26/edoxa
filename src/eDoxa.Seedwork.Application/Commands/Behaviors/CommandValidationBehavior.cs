// Filename: CommandValidationBehavior.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Behaviors
{
    public sealed class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public CommandValidationBehavior(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        [ItemCanBeNull]
        public async Task<TResponse> Handle([NotNull] TRequest command, CancellationToken cancellationToken, [NotNull] RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators.Select(validator => validator.Validate(command))
                                                .SelectMany(result => result.Errors)
                                                .Where(failure => failure != null)
                                                .ToList();

            if (validationFailures.Any())
            {
                throw new ValidationException($"Command validation errors for type {typeof(TRequest).Name}.", validationFailures);
            }

            return await next();
        }
    }
}