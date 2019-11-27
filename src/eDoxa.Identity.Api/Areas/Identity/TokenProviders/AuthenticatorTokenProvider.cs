// Filename: AuthenticatorTokenProvider.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.TokenProviders.Options;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.TokenProviders
{
    public sealed class AuthenticatorTokenProvider : DataProtectorTokenProvider<User>
    {
        public AuthenticatorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<AuthenticatorTokenProviderOptions> options,
            ILogger<AuthenticatorTokenProvider> logger
        ) : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
