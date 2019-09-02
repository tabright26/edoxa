// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-09-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Security;
using eDoxa.Swagger.Extensions;
using eDoxa.Swagger.Filters;
using eDoxa.Swagger.Options;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(
            this IServiceCollection services,
            string xmlCommentsFilePath,
            IHasApiResourceAppSettings apiResourceAppSettings,
            IHasAuthorityAppSettings authorityAppSettings,
            params KeyValuePair<string, string>[] scopes
        )
        {
            services.AddSwagger(
                authorityAppSettings.Authority.PublicUrl,
                apiResourceAppSettings.ApiResource.Name,
                apiResourceAppSettings.ApiResource.DisplayName,
                apiResourceAppSettings.ApiResource.Description,
                xmlCommentsFilePath,
                options =>
                {
                    options.DescribeAllEnumerationsAsStrings();

                    options.OperationFilter<SwaggerOperationFilter>(
                        new SwaggerOperationOptions(Scopes.IdentityApi, Scopes.CashierApi, Scopes.ArenaChallengesApi));
                },
                scopes);
        }
    }
}
