// Filename: IdentityApiWeb.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;

using Autofac;

using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Modules;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.Seedwork.Testing
{
    public abstract class IdentityApiWeb<TStartup> : WebApiFactory<TStartup>
    where TStartup : class
    {
        public override WebApplicationFactory<TStartup> WithClaims(params Claim[] claims)
        {
            return this.WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddFakeAuthenticationFilter(claims)));
        }
    }
}
