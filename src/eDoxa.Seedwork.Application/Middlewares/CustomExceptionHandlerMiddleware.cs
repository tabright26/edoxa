// Filename: CustomExceptionHandlerMiddleware.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Application.Middlewares
{
    public sealed class CustomExceptionHandlerMiddleware
    {
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(IHostingEnvironment environment, ILogger<CustomExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _environment = environment;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(new EventId(exception.HResult), exception, exception.Message);

                var error = new Error();

                if (_environment.IsDevelopment())
                {
                    error = new Error(exception);
                }

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteJsonAsync(error.ToString());
            }
        }
    }
}
