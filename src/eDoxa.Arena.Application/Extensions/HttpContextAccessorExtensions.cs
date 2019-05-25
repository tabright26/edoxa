﻿// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Domain;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        [CanBeNull]
        public static ExternalAccount GetExternalAccount(this IHttpContextAccessor accessor, Game game)
        {
            var externalAccount = accessor.GetClaimOrDefault(game.GetClaimType());

            return externalAccount != null ? new ExternalAccount(externalAccount) : null;
        }
    }
}
