// Filename: CustomCorsPolicyService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Infrastructure;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Services
{
    internal sealed class CustomCorsPolicyService : DefaultCorsPolicyService
    {
        public CustomCorsPolicyService(
            IHostingEnvironment environment,
            IOptions<IdentityAppSettings> appSettings,
            ILogger<CustomCorsPolicyService> logger
        ) : base(logger)
        {
            AllowAll = !environment.IsProduction();

            AllowedOrigins = new List<string>
            {
                appSettings.Value.WebSpaUrl
            };
        }
    }
}
