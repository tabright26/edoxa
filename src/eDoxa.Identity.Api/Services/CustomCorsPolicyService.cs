// Filename: CustomCorsPolicyService.cs
// Date Created: 2020-02-01
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Infrastructure;

using IdentityServer4.Services;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Services
{
    public sealed class CustomCorsPolicyService : DefaultCorsPolicyService
    {
        public CustomCorsPolicyService(IOptionsSnapshot<IdentityAppSettings> appSettings, ILogger<CustomCorsPolicyService> logger) : base(logger)
        {
            AllowedOrigins = new HashSet<string>
            {
                appSettings.Value.WebSpaUrl
            };
        }
    }
}
