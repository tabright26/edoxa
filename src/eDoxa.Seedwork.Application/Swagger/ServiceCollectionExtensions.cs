// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Security;
using eDoxa.Swagger.Extensions;
using eDoxa.Swagger.Filters;
using eDoxa.Swagger.Options;

using Google.Protobuf.WellKnownTypes;

using IdentityServer4.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
                    options.MapType<DecimalValue>(
                        () => new OpenApiSchema
                        {
                            Type = "number"
                        });

                    options.MapType<Timestamp>(
                        () => new OpenApiSchema
                        {
                            Type = "integer"
                        });

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
