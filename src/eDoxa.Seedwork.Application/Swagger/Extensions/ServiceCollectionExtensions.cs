// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using eDoxa.Seedwork.Application.Swagger.Filters;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSecurityDefinition(this SwaggerGenOptions options, AppSettings appSettings)
        {
            options.AddSecurityDefinition(
                "oauth2",
                new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{appSettings.Authority.PublicUrl}/connect/authorize",
                    TokenUrl = $"{appSettings.Authority.PublicUrl}/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                        [appSettings.ApiResource.Name] = appSettings.ApiResource.DisplayName
                    }
                }
            );
        }

        public static Info CreateInfoForApiVersion(this ApiVersionDescription description, AppSettings appSettings)
        {
            var info = new Info
            {
                Title = appSettings.ApiResource.DisplayName,
                Version = description.GroupName,
                Description = appSettings.ApiResource?.Description,
                Contact = new Contact
                {
                    Name = "Francis Quenneville",
                    Email = "francis@edoxa.gg"
                },
                TermsOfService = "eDoxa"
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        public static bool IsValid<T>(this T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var validationResult = new List<ValidationResult>();

            var result = Validator.TryValidateObject(data, new ValidationContext(data), validationResult, false);

            if (!result)
            {
                foreach (var item in validationResult)
                {
                    Debug.WriteLine($"ERROR::{item.MemberNames}:{item.ErrorMessage}");
                }
            }

            return result;
        }

        public static AppSettings ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration, ApiResource apiResource)
        {
            var appSettingsSection = configuration.GetSection(nameof(AppSettings));

            if (appSettingsSection == null)
            {
                throw new Exception($"No {nameof(AppSettings)} section has been found.");
            }

            var appSettings = appSettingsSection.Get<AppSettings>();

            appSettings.ApiResource = apiResource;

            if (!appSettings.IsValid())
            {
                throw new Exception("No valid settings.");
            }

            services.Configure<AppSettings>(appSettingsSection);

            return appSettings;
        }

        public static void AddFilters(this SwaggerGenOptions options)
        {
            options.DescribeAllEnumerationsAsStrings();

            options.OperationFilter<CustomOperationFilter>();

            options.DocumentFilter<CustomDocumentFilter>();

            options.CustomOperationIds(
                description =>
                {
                    if (description.ActionDescriptor is ControllerActionDescriptor descriptor)
                    {
                        return descriptor.ControllerName + descriptor.ActionName;
                    }

                    return description.ActionDescriptor.DisplayName;
                }
            );

            options.TagActionsBy(
                description =>
                {
                    if (description.ActionDescriptor is ControllerActionDescriptor descriptor)
                    {
                        var tags = descriptor.ControllerTypeInfo.GetCustomAttributes<ApiExplorerSettingsAttribute>()
                            .Where(attribute => !attribute.IgnoreApi)
                            .Select(attribute => attribute.GroupName)
                            .ToList();

                        if (tags.Any())
                        {
                            return tags;
                        }

                        throw new Exception(
                            $"Each controller must have the attribute: {nameof(ApiExplorerSettingsAttribute)}. The attribute is missing for the controller: {descriptor.ControllerName}."
                        );
                    }

                    return Array.Empty<string>();
                }
            );
        }
    }
}
