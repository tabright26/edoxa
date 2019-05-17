﻿// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Swagger.Filters;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;

namespace eDoxa.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            ApiResource apiResource)
        {
            if (!environment.IsDevelopment())
            {
                return;
            }

            var authority = configuration["Authority"];

            var assembly = Assembly.GetCallingAssembly();

            services.AddSwaggerGen(
                options =>
                {
                    options.DescribeAllEnumerationsAsStrings();

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(
                            description.GroupName,
                            new Info
                            {
                                Title = apiResource.DisplayName,
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

                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));

                    options.AddSecurityDefinition(
                        "oauth2",
                        new OAuth2Scheme
                        {
                            Type = "oauth2",
                            Flow = "implicit",
                            AuthorizationUrl = authority + "/connect/authorize",
                            TokenUrl = authority + "/connect/token",
                            Scopes = new Dictionary<string, string>
                            {
                                [apiResource.Name] = apiResource.DisplayName
                            }
                        }
                    );

                    options.OperationFilter<CustomOperationFilter>();

                    options.TagActionsBy(apiDescription =>
                    {
                        if (apiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
                        {
                            var tags = descriptor.ControllerTypeInfo
                                .GetCustomAttributes<ApiExplorerSettingsAttribute>()
                                .Where(attribute => !attribute.IgnoreApi)
                                .Select(attribute => attribute.GroupName)
                                .ToList();

                            if (tags.Any())
                            {
                                return tags;
                            }
                        }

                        throw new Exception($"Each controller must have the attribute: {nameof(ApiExplorerSettingsAttribute)}." +
                                            (apiDescription.ActionDescriptor is ControllerActionDescriptor controller
                                                ? $" The attribute is missing for the controller: {controller.ControllerName}."
                                                : string.Empty));
                    });
                }
            );
        }
    }
}