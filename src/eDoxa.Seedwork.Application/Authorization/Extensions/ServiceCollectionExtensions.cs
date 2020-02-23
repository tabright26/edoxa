// Filename: ServiceCollectionExtensions.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using IdentityModel;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Authorization.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(
                options =>
                {
                    options.AddPolicy(
                        AuthorizationPolicies.EmailVerified,
                        policy => policy.RequireClaim(JwtClaimTypes.EmailVerified, bool.TrueString.ToLowerInvariant()));
                });
        }
    }
}
