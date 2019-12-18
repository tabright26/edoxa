// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-29
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

namespace eDoxa.Seedwork.Application.Swagger
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
                authorityAppSettings.Authority,
                apiResourceAppSettings.ApiResource.Name,
                apiResourceAppSettings.ApiResource.DisplayName,
                apiResourceAppSettings.ApiResource.Description,
                xmlCommentsFilePath,
                options =>
                {
                    options.EnableAnnotations(true);

                    options.DescribeAllEnumerationsAsStrings();

                    options.OperationFilter<SecurityRequirementsOperationFilter>(
                        new SwaggerOperationOptions(
                            Scopes.IdentityApi,
                            Scopes.PaymentApi,
                            Scopes.CashierApi,
                            Scopes.NotificationsApi,
                            Scopes.ChallengesApi,
                            Scopes.GamesApi,
                            Scopes.ClansApi,
                            Scopes.ChallengesWebAggregator,
                            Scopes.CashierWebAggregator));

                    //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    //{
                    //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    //    Name = "Authorization",
                    //    In = ParameterLocation.Header,
                    //    Type = SecuritySchemeType.ApiKey
                    //});
                },
                scopes);
        }
    }
}
