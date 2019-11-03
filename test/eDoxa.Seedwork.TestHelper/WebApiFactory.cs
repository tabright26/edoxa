// Filename: WebApiFactory.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;

using Autofac;

using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Modules;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.TestHelper
{
    public abstract class WebApiFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(this.ConfigureTestServices);
            
            builder.ConfigureTestContainer<ContainerBuilder>(this.ContainerTestBuilder);
        }

        public virtual WebApplicationFactory<TStartup> WithClaims(params Claim[] claims)
        {
            return this.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(services => services.AddFakeAuthentication(options => options.Claims = claims));

                    builder.ConfigureTestContainer<ContainerBuilder>(container => container.RegisterModule(new MockHttpContextAccessorModule(claims)));
                }
            );
        }

        protected abstract void ConfigureTestServices(IServiceCollection services);

        protected abstract void ContainerTestBuilder(ContainerBuilder builder);
    }
}
