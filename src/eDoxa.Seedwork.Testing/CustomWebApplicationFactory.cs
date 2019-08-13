// Filename: CustomWebApplicationFactory.cs
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
    public abstract class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        public WebApplicationFactory<TStartup> WithClaimsPrincipal()
        {
            return this.WithClaimsPrincipal(new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()));
        }

        public WebApplicationFactory<TStartup> WithClaimsPrincipal(params Claim[] claims)
        {
            return this.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(services => services.AddFakeClaimsPrincipalFilter(claims));

                    builder.ConfigureTestContainer<ContainerBuilder>(container => container.RegisterModule(new MockHttpContextAccessorModule(claims)));
                }
            );
        }
    }
}
