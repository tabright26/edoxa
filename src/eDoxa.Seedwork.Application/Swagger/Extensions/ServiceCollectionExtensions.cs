// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Reflection;

using eDoxa.Seedwork.Application.Swagger.Filters;
using eDoxa.Seedwork.Monitoring.AppSettings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services, string xmlCommentsFilePath, IHasApiResourceAppSettings appSettings)
        {
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV");

            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDocs(services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>(), appSettings);

                    options.IncludeXmlComments(xmlCommentsFilePath);

                    options.AddSecurityDefinition(appSettings);

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
            );
        }
    }
}
