// Filename: CustomCorsPolicyService.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using IdentityServer4.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Services
{
    internal sealed class CustomCorsPolicyService : DefaultCorsPolicyService
    {
        public CustomCorsPolicyService(IHostingEnvironment environment, ILogger<CustomCorsPolicyService> logger) : base(logger)
        {
            AllowAll = !environment.IsProduction();
        }
    }
}
