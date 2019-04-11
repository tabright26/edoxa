// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Filters;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddVersioning(this IServiceCollection services, ApiVersion defaultApiVersion)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = defaultApiVersion;
                    options.ReportApiVersions = true;
                }
            );

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV");
        }

        public static void AddMvc(this IServiceCollection services)
        {
            var builder = services.AddMvc(
                options =>
                {
                    //TODO: Can be merge with Security DelegatingHandlers
                    options.Filters.Add<CustomActionFilter>();
                }
            );

            builder.AddControllersAsServices();

            builder.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void AddMvcWithApiBehavior(this IServiceCollection services)
        {
            services.AddMvc();

            services.ConfigureApiBehavior();
        }

        private static void ConfigureApiBehavior(this IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var validationProblemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status400BadRequest,
                            Detail = "Please refer to the errors property for additional details."
                        };

                        return new BadRequestObjectResult(validationProblemDetails)
                        {
                            ContentTypes =
                            {
                                "application/problem+json", "application/problem+xml"
                            }
                        };
                    };
                }
            );
        }
    }
}