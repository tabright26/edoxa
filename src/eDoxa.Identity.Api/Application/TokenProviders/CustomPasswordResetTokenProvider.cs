// Filename: CustomPasswordResetTokenProvider.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.TokenProviders.Options;
using eDoxa.Identity.Api.Models;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.TokenProviders
{
    public sealed class CustomPasswordResetTokenProvider : DataProtectorTokenProvider<UserModel>
    {
        public CustomPasswordResetTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomPasswordResetTokenProviderOptions> options
        ) : base(dataProtectionProvider, options)
        {
        }
    }
}
