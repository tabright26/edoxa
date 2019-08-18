// Filename: ExceptionHandlerMiddleware.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application
{
    // TODO: This middleware must be replaced by the default Exception Handler Middleware in .Net Core 3.0.
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(IHostingEnvironment environment, ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
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

                // Content-Type is normally included in the HTTP response and Accept is normally included in the HTTP request.
                // Content-Type and Accept HTTP headers are respectively the server response body type and the server accept body type.
                // For more control over HTTP requests use a custom CORS middleware.
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteJsonAsync(error.ToString());
            }
        }

        [JsonObject]
        private sealed class Error // These Json attribute configurations are valid because this class is never deserialized.
        {
            public Error(Exception exception)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
                Message = exception.Message;
            }

            public Error()
            {
                StatusCode = StatusCodes.Status500InternalServerError;
                Message = "Internal Server Error";
            }

            [JsonProperty("statusCode")]
            public int StatusCode { get; }

            [JsonProperty("message")]
            public string Message { get; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
