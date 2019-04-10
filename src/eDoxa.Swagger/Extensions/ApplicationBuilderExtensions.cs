// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace eDoxa.Swagger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder application, IApiVersionDescriptionProvider provider, Action<SwaggerClientConfig> config)
        {
            var swaggerClientConfig = new SwaggerClientConfig();

            config.Invoke(swaggerClientConfig);

            application.UseSwagger();

            application.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                    }

                    options.OAuthClientId(swaggerClientConfig.Id);
                    options.OAuthAppName(swaggerClientConfig.Name);
                    options.DefaultModelExpandDepth(0);
                    options.DefaultModelsExpandDepth(-1);
                }
            );
        }

        public static void UseSwaggerWithRedirects(
            this IApplicationBuilder application,
            IApiVersionDescriptionProvider provider,
            Action<SwaggerClientConfig> config)
        {
            application.UseSwagger(provider, config);

            application.UseStatusCodePagesWithRedirects("~/swagger");
        }
    }
}