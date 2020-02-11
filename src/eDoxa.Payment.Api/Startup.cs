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

using eDoxa.Grpc.Protos.Payment.Options;
using eDoxa.Payment.Api.Grpc.Services;
using eDoxa.Payment.Api.Infrastructure;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Paypal.Extensions;
using eDoxa.Paypal.Services.Abstractions;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.Autofac.Extensions;
using eDoxa.Seedwork.Application.AutoMapper.Extensions;
using eDoxa.Seedwork.Application.Cors.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.FluentValidation;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Application.HealthChecks.Extensions;
using eDoxa.Seedwork.Application.Middlewares.ProblemDetails.Extensions;
using eDoxa.Seedwork.Application.Swagger;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.ServiceBus.Azure.Extensions;
using eDoxa.ServiceBus.TestHelper.Extensions;
using eDoxa.Stripe.Extensions;
using eDoxa.Stripe.Services.Abstractions;

using FluentValidation;

using Hellang.Middleware.ProblemDetails;

using IdentityServer4.AccessTokenValidation;

using MediatR;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api
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
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetAppSettings<PaymentAppSettings>(ApiResources.PaymentApi);
        }

        public IConfiguration Configuration { get; }

        public PaymentAppSettings AppSettings { get; }
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings<PaymentAppSettings>(Configuration);

            services.Configure<PaymentApiOptions>(Configuration.GetSection("Api"));

            services.AddStripe(Configuration);

            services.AddPaypal(Configuration);

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomIdentityServer(AppSettings)
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomAzureServiceBusTopic(Configuration);

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails(
                options =>
                {
                    options.MapStripeException();
                    options.MapPaypalException();
                });

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(Startup));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = AppSettings.ApiResource.Name;
                        options.Authority = AppSettings.Endpoints.IdentityUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAzureServiceBusModule<Startup>(AppServices.PaymentApi);
        }

        public void Configure(IApplicationBuilder application, IServiceBusSubscriber subscriber)
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
                    endpoints.MapGrpcService<PaymentGrpcService>();

                    endpoints.MapControllers();

                    endpoints.MapCustomHealthChecks();
                });

            subscriber.UseIntegrationEventSubscriptions();
        }
    }

    public partial class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.AddSwagger(XmlCommentsFilePath, AppSettings, AppSettings);
        }

        public void ConfigureDevelopment(IApplicationBuilder application, IServiceBusSubscriber subscriber)
        {
            this.Configure(application, subscriber);

            application.UseSwagger(AppSettings);
        }
    }

    public partial class Startup
    {
        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddAppSettings<PaymentAppSettings>(Configuration);

            services.Configure<PaymentApiOptions>(Configuration.GetSection("Api"));

            services.AddCustomCors();

            services.AddCustomGrpc();

            services.AddCustomProblemDetails(
                options =>
                {
                    options.MapStripeException();
                    options.MapPaypalException();
                });

            services.AddCustomControllers<Startup>();

            services.AddCustomApiVersioning(new ApiVersion(1, 0));

            services.AddCustomAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(Startup));

            services.AddAuthentication();
        }

        public void ConfigureTestContainer(ContainerBuilder builder)
        {
            builder.RegisterMockServiceBusModule();

            builder.RegisterMock<IStripeCustomerService>();

            builder.RegisterMock<IStripeInvoiceItemService>();

            builder.RegisterMock<IStripeInvoiceService>();

            builder.RegisterMock<IStripePaymentMethodService>();

            builder.RegisterMock<IPaypalPayoutService>();
        }

        public void ConfigureTest(IApplicationBuilder application, IServiceBusSubscriber subscriber)
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
                    endpoints.MapGrpcService<PaymentGrpcService>();

                    endpoints.MapControllers();
                });

            subscriber.UseIntegrationEventSubscriptions();
        }
    }
}
