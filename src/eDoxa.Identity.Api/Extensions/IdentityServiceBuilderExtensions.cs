// Filename: IdentityServiceBuilderExtensions.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Services;

using IdentityServer4.Services;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
{
    public static class IdentityServiceBuilderExtensions
    {
        public static void BuildCustomServices(this IIdentityServerBuilder builder)
        {
            var services = builder.Services;
            services.AddTransient<ICorsPolicyService, CustomCorsPolicyService>();
            services.AddTransient<IProfileService, CustomProfileService>();
        }
    }
}
