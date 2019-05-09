// Filename: ExternalLoginService.cs
// Date Created: 2019-05-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using eDoxa.Security.Abstractions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Security.Services
{
    public sealed class UserLoginInfoService : IUserLoginInfoService
    {
        private readonly IEnumerable<Claim> _claims;

        public UserLoginInfoService(IHttpContextAccessor accessor)
        {
            _claims = accessor?.HttpContext?.User?.Claims ??
                      throw new ArgumentNullException(nameof(accessor), "IHttpContextAccessor was injected from an invalid HttpContext.");
        }

        public string GetExternalKey(Game game)
        {
            return _claims.SingleOrDefault(claim => claim.Type.Contains(game.GetClaimType()))?.Value;
        }
    }
}