// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Security;
using eDoxa.Swagger.Extensions;
using eDoxa.Swagger.Filters;
using eDoxa.Swagger.Options;

using IdentityServer4.Models;

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
            params Scope[] scopes
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
                        new SwaggerOperationOptions(
                            Scopes.IdentityApi,
                            Scopes.PaymentApi,
                            Scopes.CashierApi,
                            Scopes.ArenaChallengesApi,
                            Scopes.ArenaGamesApi,
                            Scopes.ArenaGamesLeagueOfLegendsApi,
                            Scopes.OrganizationsClansApi));
                },
                scopes);
        }
    }
}
