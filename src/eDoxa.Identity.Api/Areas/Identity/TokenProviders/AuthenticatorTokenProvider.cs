// Filename: CustomAuthenticatorTokenProvider.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.TokenProviders.Options;
using eDoxa.Identity.Api.Models;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.TokenProviders
{
    public sealed class AuthenticatorTokenProvider : DataProtectorTokenProvider<User>
    {
        public AuthenticatorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<AuthenticatorTokenProviderOptions> options
        ) : base(dataProtectionProvider, options)
        {
        }
    }
}
