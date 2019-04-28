// Filename: ValidationExceptionFilter.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using FluentValidation;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Seedwork.Application.Filters
{
    public sealed class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException([NotNull] ExceptionContext context)
        {
            if (!(context.Exception is ValidationException exception))
            {
                return;
            }

            var errors = exception.Errors.ToDictionary(failure => failure.PropertyName, failure => new[] {failure.ErrorMessage});

            var error = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = "Please refer to the errors property for additional details."
            };

            context.Result = new BadRequestObjectResult(error);
        }
    }
}