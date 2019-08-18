// Filename: WebApiFactory.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;

using Autofac;

using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Modules;

using IdentityModel;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.Seedwork.Testing
{
    public abstract class WebApiFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
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
                }
            );
        }
    }
}
