// Filename: Startup.cs
// Date Created: 2019-11-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;

using Autofac;

using AutoMapper;

using eDoxa.Challenges.Web.Aggregator.Infrastructure;
using eDoxa.Challenges.Web.Aggregator.Services;
using eDoxa.Seedwork.Application.DelegatingHandlers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Validations;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Azure.Modules;

using FluentValidation;
using FluentValidation.AspNetCore;

using HealthChecks.UI.Client;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Polly;
using Polly.Extensions.Http;

using Refit;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Challenges.Web.Aggregator
{
    public sealed class Startup
    {
        private static readonly string XmlCommentsFilePath = Path.Combine(
            AppContext.BaseDirectory,
            $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml");

        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
            ValidatorOptions.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<ChallengesWebAggregatorAppSettings>(ChallengesWebAggregator);
        }

        private ChallengesWebAggregatorAppSettings AppSettings { get; }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(message => message.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<ChallengesWebAggregatorAppSettings>(Configuration);

            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddAzureKeyVault(Configuration)
                .AddAzureServiceBusTopic(Configuration)
                .AddUrlGroup(AppSettings.Endpoints.IdentityUrl, AppNames.IdentityApi)
                .AddUrlGroup(AppSettings.Endpoints.CashierUrl, AppNames.CashierApi)
                .AddUrlGroup(AppSettings.Endpoints.PaymentUrl, AppNames.PaymentApi)
                .AddUrlGroup(AppSettings.Endpoints.NotificationsUrl, AppNames.NotificationsApi)
                .AddUrlGroup(AppSettings.Endpoints.ChallengesUrl, AppNames.ChallengesApi)
                .AddUrlGroup(AppSettings.Endpoints.GamesUrl, AppNames.GamesApi)
                .AddUrlGroup(AppSettings.Endpoints.ClansUrl, AppNames.ClansApi);

            services.AddCors(
                options =>
                {
                    options.AddPolicy("default", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true));
                });

            services.AddMvc(
                    options =>
                    {
                        options.Filters.Add(new ProducesAttribute("application/json"));
                    })
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddControllersAsServices()
                .AddFluentValidation(
                    config =>
                    {
                        config.RegisterValidatorsFromAssemblyContaining<Startup>();
                        config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    });

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new HeaderApiVersionReader();
                });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));

            services.AddMediatR(Assembly.GetAssembly(typeof(Startup)));

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
                Scopes.GamesApi,
                Scopes.ChallengesApi);

            services.AddHttpContextAccessor();

            services.AddTransient<AccessTokenDelegatingHandler>();

            var refitSettings = new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
            };

            services.AddRefitClient<ICashierService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.CashierUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddRefitClient<IChallengesService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.ChallengesUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddRefitClient<IClansService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.ClansUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddRefitClient<IGamesService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.GamesUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddRefitClient<IIdentityService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.IdentityUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddRefitClient<INotificationsService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.NotificationsUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddRefitClient<IPaymentService>(refitSettings)
                .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(AppSettings.Endpoints.PaymentUrl))
                .AddHttpMessageHandler<AccessTokenDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AzureServiceBusModule<Startup>(Configuration.GetAzureServiceBusConnectionString()!, AppNames.ChallengesWebAggregator));
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
        {
            application.UseCustomExceptionHandler();

            application.UsePathBase(Configuration["ASPNETCORE_PATHBASE"]);

            application.UseCors("default");

            application.UseAuthentication();

            application.UseHttpsRedirection();

            application.UseMvc();

            application.UseHealthChecks(
                "/liveness",
                new HealthCheckOptions
                {
                    Predicate = registration => registration.Name.Contains("liveness")
                });

            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            application.UseSwagger(provider, AppSettings);
        }
    }
}
