// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using eDoxa.Swagger.Filters;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace eDoxa.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            Assembly assembly)
        {
            if (!environment.IsDevelopment())
            {
                return services;
            }

            var authority = configuration["Authority"];

            var apiResourceName = configuration["IdentityServer:ApiResource:Name"];

            var apiResourceDisplayName = configuration["IdentityServer:ApiResource:DisplayName"];

            services.AddSwaggerGen(
                swaggerGenOptions =>
                {
                    swaggerGenOptions.DescribeAllEnumsAsStrings();

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        swaggerGenOptions.SwaggerDoc(
                            description.GroupName,
                            new Info
                            {
                                Title = apiResourceDisplayName,
                                Version = description.GroupName,
                                Description = description.IsDeprecated ? "This API version has been deprecated." : null,
                                Contact = new Contact
                                {
                                    Name = "Francis Quenneville", Email = "francis@edoxa.gg"
                                },
                                License = new License
                                {
                                    Name = "MIT", Url = "https://opensource.org/licenses/MIT"
                                },
                                TermsOfService = "None"
                            }
                        );
                    }

                    swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));

                    swaggerGenOptions.AddSecurityDefinition(
                        "oauth2",
                        new OAuth2Scheme
                        {
                            Type = "oauth2",
                            Flow = "implicit",
                            AuthorizationUrl = authority + "/connect/authorize",
                            TokenUrl = authority + "/connect/token",
                            Scopes = new Dictionary<string, string>
                            {
                                [apiResourceName] = apiResourceDisplayName
                            }
                        }
                    );

                    swaggerGenOptions.DocumentFilter<CustomDocumentFilter>();

                    swaggerGenOptions.OperationFilter<CustomOperationFilter>();
                }
            );

            return services;
        }
    }
}