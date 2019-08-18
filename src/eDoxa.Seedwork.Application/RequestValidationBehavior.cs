// Filename: IRequestValidationBehavior.cs
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

using MediatR;

using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application
{
    public sealed class RequestValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    {
        private readonly ILogger<RequestValidationBehavior<TRequest, TResult>> _logger;
        private readonly IValidator<TRequest>[] _validators;

        public RequestValidationBehavior(IValidator<TRequest>[] validators, ILogger<RequestValidationBehavior<TRequest, TResult>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        
        public async Task<TResult> Handle( TRequest request, CancellationToken cancellationToken,  RequestHandlerDelegate<TResult> next)
        {
            _logger.LogInformation($"Validating {typeof(TRequest).Name}...");

            var errors = _validators.Select(validator => validator.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (!errors.Any())
            {
                return await next();
            }

            _logger.LogWarning($"Validation failed for {typeof(TRequest).Name}.");

            throw new ValidationException(errors);
        }
    }
}
