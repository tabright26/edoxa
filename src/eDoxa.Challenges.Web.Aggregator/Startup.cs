// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Challenges.Web.Aggregator.Infrastructure;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.Cors.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.HealthChecks.Extensions;
using eDoxa.Seedwork.Application.Http.DelegatingHandlers;
using eDoxa.Seedwork.Application.Middlewares.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.Swagger;

using FluentValidation;

using Grpc.Core;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static eDoxa.Seedwork.Application.ApiResources;

namespace eDoxa.Challenges.Web.Aggregator
{
    public partial class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml");

        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
            ValidatorOptions.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); // TODO: Check for security is required.
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetAppSettings<ChallengesWebAggregatorAppSettings>(ChallengesWebAggregator);
        }

        public IConfiguration Configuration { get; }

        private ChallengesWebAggregatorAppSettings AppSettings { get; }
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<ChallengesWebAggregatorAppSettings>(Configuration);

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomUrlGroup(AppSettings.Endpoints.IdentityUrl, AppServices.IdentityApi)
                .AddCustomUrlGroup(AppSettings.Endpoints.CashierUrl, AppServices.CashierApi)
                .AddCustomUrlGroup(AppSettings.Endpoints.ChallengesUrl, AppServices.ChallengesApi)
                .AddCustomUrlGroup(AppSettings.Endpoints.GamesUrl, AppServices.GamesApi);

            services.AddCustomCors();

            services.AddCustomProblemDetails(options => options.MapRpcException());

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = AppSettings.ApiResource.Name;
                        options.Authority = AppSettings.Endpoints.IdentityUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });

            services.AddHttpContextAccessor();

            services.AddTransient<AccessTokenDelegatingHandler>();

            services.AddGrpcClient<CashierService.CashierServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.CashierUrl}:81"))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();

            services.AddGrpcClient<IdentityService.IdentityServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.IdentityUrl}:81"))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();

            services.AddGrpcClient<ChallengeService.ChallengeServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.ChallengesUrl}:81"))
                .ConfigureChannel(
                    options =>
                    {
                        options.Credentials = ChannelCredentials.Insecure;
                        options.MaxReceiveMessageSize = 1000000000;
                    })
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();

            services.AddGrpcClient<GameService.GameServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.GamesUrl}:81"))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseProblemDetails();

            application.UseCustomPathBase();

            application.UseRouting();
            application.UseCustomCors();

            application.UseAuthentication();
            application.UseAuthorization();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();

                    endpoints.MapCustomHealthChecks();
                });
        }
    }

    public partial class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(
                XmlCommentsFilePath,
                AppSettings,
                AppSettings,
                Scopes.CashierApi,
                Scopes.GamesApi,
                Scopes.ChallengesApi);
        }

        public void ConfigureDevelopment(IApplicationBuilder application)
        {
            this.Configure(application);

            application.UseSwagger(AppSettings);
        }
    }

    public partial class Startup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddCustomCors();

            services.AddCustomProblemDetails(options => options.MapRpcException());

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup));

            services.AddAuthentication();
        }

        public void ConfigureTest(IApplicationBuilder application)
        {
            application.UseProblemDetails();

            application.UseCustomPathBase();

            application.UseRouting();
            application.UseCustomCors();

            application.UseAuthentication();
            application.UseAuthorization();

            application.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
