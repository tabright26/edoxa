// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Net.Mime;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services, ApiVersion version)
        {
            return services.AddApiVersioning(
                    options =>
                    {
                        options.ReportApiVersions = true;
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.DefaultApiVersion = version;
                        options.ApiVersionReader = new HeaderApiVersionReader();
                    })
                .AddVersionedApiExplorer();
        }

        public static IMvcBuilder AddCustomControllers<TStartup>(this IServiceCollection services)
        {
            return services.AddControllers(
                    options =>
                    {
                        options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
                        options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                    })
                .AddCustomNewtonsoftJson()
                .AddCustomFluentValidation<TStartup>();
        }

        public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            var environment = provider.GetRequiredService<IWebHostEnvironment>();

            return services.AddProblemDetails(
                options =>
                {
                    // Don't include exception details in a production environment.
                    options.IncludeExceptionDetails = context => !environment.IsProduction();

                    // This will map NotImplementedException to the 501 Not Implemented status code.
                    options.Map<NotImplementedException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status501NotImplemented));

                    // This will map HttpRequestException to the 503 Service Unavailable status code.
                    options.Map<HttpRequestException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status503ServiceUnavailable));

                    // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
                    // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
                    options.Map<Exception>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status500InternalServerError));
                });
        }
    }
}
