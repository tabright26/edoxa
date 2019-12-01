// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Swagger.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder application, IHasApiResourceAppSettings appSettings)
        {
            application.UseSwagger(
                application.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>(),
                appSettings.ApiResource.GetSwaggerClientId(),
                appSettings.ApiResource.GetSwaggerClientName());
        }
    }
}
