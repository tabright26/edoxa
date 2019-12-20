// Filename: WebHostFactory.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Security.Claims;

using Autofac;

using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Modules;
using eDoxa.ServiceBus.TestHelper.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.TestHelper
{
    public abstract class WebHostFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.UseContentRoot(Directory.GetCurrentDirectory());

            builder.ConfigureTestServices(this.ConfigureTestServices);

            builder.ConfigureTestContainer<ContainerBuilder>(this.ContainerTestBuilder);
        }

        public WebApplicationFactory<TStartup> WithDefaultClaims()
        {
            return this.WithClaims(new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()));
        }

        public virtual WebApplicationFactory<TStartup> WithClaims(params Claim[] claims)
        {
            return this.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(services => services.AddFakeAuthentication(options => options.Claims = claims));

                    builder.ConfigureTestContainer<ContainerBuilder>(container => container.RegisterModule(new MockHttpContextAccessorModule(claims)));
                });
        }

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
        }

        protected virtual void ContainerTestBuilder(ContainerBuilder builder)
        {
            builder.RegisterMockServiceBusModule();
        }
    }
}
