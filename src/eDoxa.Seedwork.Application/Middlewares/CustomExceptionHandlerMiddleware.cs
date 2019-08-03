// Filename: CustomExceptionHandlerMiddleware.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

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

                var error = new ErrorDto();

                if (_environment.IsDevelopment())
                {
                    error = new ErrorDto(exception);
                }

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteJsonAsync(error.ToString());
            }
        }

        [JsonObject]
        private sealed class ErrorDto
        {
            public ErrorDto(Exception exception)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
                Message = exception.Message;
            }

            public ErrorDto()
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
