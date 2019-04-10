// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-03-04
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

using eDoxa.Swagger.DocumentFilters;
using eDoxa.Swagger.OperationFilters;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace eDoxa.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services, string authority, string path,  Action<SwaggerApiResourceConfig> config)
        {
            var swaggerApiResourceConfig = new SwaggerApiResourceConfig();

            config.Invoke(swaggerApiResourceConfig);

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
                                Title = swaggerApiResourceConfig.ApiResourceDisplayName,
                                Version = description.GroupName,
                                Description = swaggerApiResourceConfig.ApiResourceDescription +
                                              (description.IsDeprecated ? " (This API version has been deprecated)" : string.Empty),
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

                    swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{path}.xml"));

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
                                [swaggerApiResourceConfig.ApiResourceName] = swaggerApiResourceConfig.ApiResourceDisplayName
                            }
                        }
                    );

                    swaggerGenOptions.DocumentFilter<CustomDocumentFilter>();
                    swaggerGenOptions.OperationFilter<CustomOperationFilter>();
                    swaggerGenOptions.OperationFilter<SecurityOperationFilter>();
                    swaggerGenOptions.OperationFilter<IdempotencyKeyOperationFilter>();
                }
            );
        }
    }
}