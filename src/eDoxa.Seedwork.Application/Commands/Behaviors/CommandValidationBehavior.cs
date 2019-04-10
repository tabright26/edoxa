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

using MediatR;

namespace eDoxa.Seedwork.Application.Commands.Behaviors
{
    /// <summary>
    ///     Enable tracking of validator behavior for the application monitoring feature.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="IPipelineBehavior{TRequest,TResponse}" />
    public sealed class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public CommandValidationBehavior(IValidator<TRequest>[] validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
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