// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder application, IApiVersionDescriptionProvider provider, IHasApiResourceAppSettings appSettings)
        {
            application.UseSwagger();

            application.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                    }

                    options.RoutePrefix = string.Empty;

                    options.OAuthClientId(appSettings.ApiResource.SwaggerClientId());

                    options.OAuthAppName(appSettings.ApiResource.SwaggerClientName());

                    options.DefaultModelExpandDepth(0);

                    options.DefaultModelsExpandDepth(-1);
                }
            );
        }
    }
}
