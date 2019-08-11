// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;

using eDoxa.Seedwork.Testing.Mocks;

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTestMvc(this IServiceCollection services, IEnumerable<Claim> claims)
        {
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new AllowAnonymousFilter());
                    options.Filters.Add(new MockAsyncClaimsPrincipalFilter(claims));
                }
            );
        }
    }
}
