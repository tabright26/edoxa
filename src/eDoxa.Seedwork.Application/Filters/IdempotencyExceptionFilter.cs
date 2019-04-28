// Filename: IdempotencyExceptionFilter.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure.Exceptions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Seedwork.Application.Filters
{
    public sealed class IdempotencyExceptionFilter : IExceptionFilter
    {
        public void OnException([NotNull] ExceptionContext context)
        {
            if (!(context.Exception is IdempotencyException exception))
            {
                return;
            }

            context.Result = new ConflictObjectResult(exception.Message);
        }
    }
}