// Filename: CustomPasswordResetTokenProvider.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Security.Models;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Seedwork.Security
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
