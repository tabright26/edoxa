// Filename: LogoutController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Domain.Services;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/logout")]
    [ApiExplorerSettings(GroupName = "Logout")]
    public sealed class LogoutController : ControllerBase
    {
        private readonly ISignInService _signInService;
        private readonly IIdentityServerInteractionService _interactionService;

        public LogoutController(ISignInService signInService, IIdentityServerInteractionService interactionService)
        {
            _signInService = signInService;
            _interactionService = interactionService;
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync([FromQuery] string logoutId)
        {
            var context = await _interactionService.GetLogoutContextAsync(logoutId);

            var showSignoutPrompt = context?.ShowSignoutPrompt != false;

            await _interactionService.RevokeTokensForCurrentSessionAsync();

            if (User?.Identity.IsAuthenticated == true)
            {
                await _signInService.SignOutAsync();

                //// delete local authentication cookie
                //await HttpContext.SignOutAsync();
            }

            // no external signout supported for now (see \Quickstart\Account\AccountController.cs TriggerExternalSignout)
            return this.Ok(
                new
                {
                    showSignoutPrompt,
                    ClientName = string.IsNullOrEmpty(context?.ClientName) ? context?.ClientId : context?.ClientName,
                    context?.PostLogoutRedirectUri,
                    context?.SignOutIFrameUrl,
                    logoutId
                });
        }
    }
}
