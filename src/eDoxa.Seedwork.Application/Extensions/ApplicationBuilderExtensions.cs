// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Swagger.Client.Extensions;
using eDoxa.Swagger.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder application, IApiVersionDescriptionProvider provider, IHasApiResourceAppSettings appSettings)
        {
            application.UseSwagger(provider, appSettings.ApiResource.GetSwaggerClientId(), appSettings.ApiResource.GetSwaggerClientName());
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
