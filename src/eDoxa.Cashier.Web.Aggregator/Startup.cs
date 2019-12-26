// Filename: Startup.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Cashier.Web.Aggregator.Infrastructure;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.DelegatingHandlers;
using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.Security.Cors.Extensions;

using FluentValidation;

using Grpc.Core;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Cashier.Web.Aggregator
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
            AppSettings = configuration.GetAppSettings<CashierWebAggregatorAppSettings>(CashierWebAggregator);
        }

        public IConfiguration Configuration { get; }

        private CashierWebAggregatorAppSettings AppSettings { get; }
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<CashierWebAggregatorAppSettings>(Configuration);

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddAzureKeyVault(Configuration)
                .AddUrlGroup(AppSettings.Endpoints.IdentityUrl, AppServices.IdentityApi)
                .AddUrlGroup(AppSettings.Endpoints.CashierUrl, AppServices.CashierApi)
                .AddUrlGroup(AppSettings.Endpoints.PaymentUrl, AppServices.PaymentApi);

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

            services.AddSwagger(
                XmlCommentsFilePath,
                AppSettings,
                AppSettings,
                Scopes.CashierApi,
                Scopes.PaymentApi);

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

            services.AddGrpcClient<PaymentService.PaymentServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.PaymentUrl}:81"))
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

                    endpoints.MapConfigurationRoute<CashierWebAggregatorAppSettings>(AppSettings.ApiResource);

                    endpoints.MapCustomHealthChecks();
                });

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
