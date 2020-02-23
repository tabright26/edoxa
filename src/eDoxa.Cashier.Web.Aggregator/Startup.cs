// Filename: Startup.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Cashier.Web.Aggregator.Infrastructure;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.Authorization.Extensions;
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
        }

        public IConfiguration Configuration { get; }

        private CashierWebAggregatorAppSettings AppSettings => Configuration.Get<CashierWebAggregatorAppSettings>();
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<CashierWebAggregatorAppSettings>().Bind(Configuration).ValidateDataAnnotations();

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomUrlGroup(AppSettings.Service.Endpoints.IdentityUrl, AppServices.IdentityApi)
                .AddCustomUrlGroup(AppSettings.Service.Endpoints.CashierUrl, AppServices.CashierApi)
                .AddCustomUrlGroup(AppSettings.Service.Endpoints.PaymentUrl, AppServices.PaymentApi);

            services.AddCustomCors();

            services.AddCustomProblemDetails(options => options.MapRpcException());

            services.AddCustomControllers<Startup>();

            services.AddCustomAuthorization();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = ApiResources.CashierWebAggregator.Name;
                        options.Authority = AppSettings.Authority.InternalUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });

            services.AddHttpContextAccessor();

            services.AddTransient<AccessTokenDelegatingHandler>();

            services.AddGrpcClient<CashierService.CashierServiceClient>(options => options.Address = new Uri(AppSettings.Grpc.Service.Endpoints.CashierUrl))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();

            services.AddGrpcClient<IdentityService.IdentityServiceClient>(options => options.Address = new Uri(AppSettings.Grpc.Service.Endpoints.IdentityUrl))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();

            services.AddGrpcClient<PaymentService.PaymentServiceClient>(options => options.Address = new Uri(AppSettings.Grpc.Service.Endpoints.PaymentUrl))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>();
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
                ApiResources.CashierWebAggregator,
                AppSettings.Authority,
                Scopes.CashierApi,
                Scopes.PaymentApi);
        }

        public void ConfigureDevelopment(IApplicationBuilder application)
        {
            this.Configure(application);

            application.UseSwagger(ApiResources.CashierWebAggregator);
        }
    }

    public partial class Startup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddCustomCors();

            services.AddCustomProblemDetails(options => options.MapRpcException());

            services.AddCustomControllers<Startup>();

            services.AddCustomAuthorization();

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
