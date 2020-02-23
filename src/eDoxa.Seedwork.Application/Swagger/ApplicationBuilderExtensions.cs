// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Swagger.Extensions;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSwagger(this IApplicationBuilder application, ApiResource apiResource)
        {
            application.UseSwagger(
                application.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>(),
                apiResource.GetSwaggerClientId(),
                apiResource.GetSwaggerClientName());
        }
    }
}
