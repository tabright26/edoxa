// Filename: WebHostFactory.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Security.Claims;

using Autofac;

using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Fakes;
using eDoxa.Seedwork.TestHelper.Modules;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.Seedwork.TestHelper
{
    public abstract class WebHostFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.UseContentRoot(Directory.GetCurrentDirectory());
        }

        public WebApplicationFactory<TStartup> WithClaimsFromDefaultAuthentication(params Claim[] claims)
        {
            return this.WithWebHostBuilder(
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

        public WebApplicationFactory<TStartup> WithClaimsFromBearerAuthentication(params Claim[] claims)
        {
            return this.WithWebHostBuilder(
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
