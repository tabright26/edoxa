// Filename: RedirectionService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity.Application.Services
{
    public class RedirectionService : IRedirectionService
    {
        private readonly IConfiguration _configuration;

        public RedirectionService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string RedirectToPrincipal()
        {
            return _configuration["IdentityServer:Clients:Web:Spa:ClientUri"];
        }
    }
}