// Filename: WebApplicationFactoryExtensions.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Security.Claims;

using Autofac;

using eDoxa.Seedwork.Application.Http.DelegatingHandlers;
using eDoxa.Seedwork.TestHelper.Modules;

using Grpc.Net.Client;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class WebApplicationFactoryExtensions
    {
        public static GrpcChannel CreateChannel<TStartup>(this WebApplicationFactory<TStartup> factory)
        where TStartup : class
        {
            var httpClient = factory.CreateDefaultClient(
                new VersionDelegatingHandler
                {
                    InnerHandler = factory.Server.CreateHandler()
                });

            return GrpcChannel.ForAddress(
                httpClient.BaseAddress,
                new GrpcChannelOptions
                {
                    HttpClient = httpClient
                });
        }

        public static WebApplicationFactory<TStartup> WithClaimsFromDefaultAuthentication<TStartup>(
            this WebApplicationFactory<TStartup> factory,
            params Claim[] claims
        )
        where TStartup : class
        {
            return factory.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(
                        services =>
                        {
                            services.AddTestAuthentication(
                                options =>
                                {
                                    options.Claims = claims;
                                    options.AuthenticationScheme = nameof(TestAuthenticationHandler);
                                });
                        });

                    builder.ConfigureTestContainer<ContainerBuilder>(container => container.RegisterModule(new MockHttpContextAccessorModule(claims)));
                });
        }

        public static WebApplicationFactory<TStartup> WithClaimsFromBearerAuthentication<TStartup>(
            this WebApplicationFactory<TStartup> factory,
            params Claim[] claims
        )
        where TStartup : class
        {
            return factory.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(
                        services =>
                        {
                            services.AddTestAuthentication(
                                options =>
                                {
                                    options.Claims = claims;
                                    options.AuthenticationScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                                });
                        });

                    builder.ConfigureTestContainer<ContainerBuilder>(container => container.RegisterModule(new MockHttpContextAccessorModule(claims)));
                });
        }
    }
}
