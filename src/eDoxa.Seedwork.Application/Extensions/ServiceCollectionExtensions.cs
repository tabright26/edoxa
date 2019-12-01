// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
