// Filename: FakeAuthenticationHandler.cs
// Date Created: 2019-08-17
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
    public class FakeAuthenticationHandler : AuthenticationHandler<FakeAuthenticationOptions>
    {
        public FakeAuthenticationHandler(
            IOptionsMonitor<FakeAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claimsIdentity = new ClaimsIdentity(Options.Claims, nameof(FakeAuthenticationHandler));

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties(), nameof(FakeAuthenticationHandler));

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
    }
}
