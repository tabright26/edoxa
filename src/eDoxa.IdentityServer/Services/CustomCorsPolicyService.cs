// Filename: CustomCorsPolicyService.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using IdentityServer4.Services;

using Microsoft.Extensions.Logging;

namespace eDoxa.IdentityServer.Services
{
    public sealed class CustomCorsPolicyService : DefaultCorsPolicyService
    {
        public CustomCorsPolicyService(ILogger<CustomCorsPolicyService> logger) : base(logger)
        {
            AllowAll = true;
        }
    }
}