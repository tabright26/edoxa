// Filename: FakeAuthenticationHandler.cs
// Date Created: 2019-12-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Seedwork.TestHelper
{
    public sealed class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
    {
        public TestAuthenticationHandler(
            IOptionsMonitor<TestAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(
            options,
            logger,
            encoder,
            clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claimsIdentity = new ClaimsIdentity(Options.Claims, Options.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties(), Options.AuthenticationScheme);

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
    }
}
