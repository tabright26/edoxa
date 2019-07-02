// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(
            this IApplicationBuilder application,
            IHostingEnvironment environment,
            IApiVersionDescriptionProvider provider,
            ApiResource apiResource
        )
        {
            if (!environment.IsDevelopment())
            {
                return;
            }

            application.UseSwagger();

            application.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                    }

                    options.RoutePrefix = string.Empty;
                    options.OAuthClientId(apiResource.SwaggerClientId());
                    options.OAuthAppName(apiResource.SwaggerClientName());
                    options.DefaultModelExpandDepth(0);
                    options.DefaultModelsExpandDepth(-1);
                }
            );
        }
    }
}
