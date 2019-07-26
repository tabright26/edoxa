// Filename: ChangePhoneNumberTokenProvider.cs
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
    public sealed class ChangePhoneNumberTokenProvider : DataProtectorTokenProvider<User>
    {
        public ChangePhoneNumberTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<ChangePhoneNumberTokenProviderOptions> options) : base(
            dataProtectionProvider,
            options
        )
        {
        }
    }
}
