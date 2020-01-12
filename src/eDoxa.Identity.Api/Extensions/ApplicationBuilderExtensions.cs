// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2020-01-12
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAuthenticationLoginRedirects(this IApplicationBuilder application)
        {
            return application.UseStatusCodePagesWithRedirects(
                application.ApplicationServices.GetRequiredService<IRedirectService>().RedirectToWebSpa("/authentication/login"));
        }
    }
}
