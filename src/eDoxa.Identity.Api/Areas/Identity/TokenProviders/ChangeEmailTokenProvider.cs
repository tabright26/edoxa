﻿// Filename: ChangeEmailTokenProvider.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.TokenProviders.Options;
using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.TokenProviders
{
    public sealed class ChangeEmailTokenProvider : DataProtectorTokenProvider<User>
    {
        public ChangeEmailTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<ChangeEmailTokenProviderOptions> options) : base(
            dataProtectionProvider,
            options
        )
        {
        }
    }
}