// Filename: Logout.cshtml.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;

using IdentityModel;

using IdentityServer4;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly UserService _userService;
        private readonly SignInService _signInService;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(
            IIdentityServerInteractionService interactionService,
            UserService userService,
            SignInService signInService,
            ILogger<LogoutModel> logger)
        {
            _interactionService = interactionService;
            _userService = userService;
            _signInService = signInService;
            _logger = logger;
        }

        [BindProperty]
        public string LogoutId { get; set; }

        public async Task<IActionResult> OnGet(string logoutId)
        {
            LogoutId = logoutId;

            if (!User.Identity.IsAuthenticated)
            {
                // if the user is not authenticated, then just show logged out page
                return await this.OnPost();
            }

            //Test for Xamarin. 
            var request = await _interactionService.GetLogoutContextAsync(LogoutId);

            if (request?.ShowSignoutPrompt == false)
            {
                //it's safe to automatically sign-out
                return await this.OnPost();
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return this.Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? "/";

            var scheme = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

            if (scheme != null && scheme != IdentityServerConstants.LocalIdentityProvider)
            {
                // if there's no current logout context, we need to create one
                // this captures necessary info from the current logged in user
                // before we sign out and redirect away to the external IdP for sign out
                var logoutId = LogoutId ?? await _interactionService.CreateLogoutContextAsync();

                try
                {
                    await HttpContext.SignOutAsync(
                        scheme,
                        new AuthenticationProperties
                        {
                            RedirectUri = "/Identity/Account/Logout?logoutId=" + logoutId
                        }
                    );
                }
                catch (Exception exception)
                {
                    _logger.LogCritical(new EventId(exception.HResult), exception, exception.Message);
                }
            }

            var user = await _userService.GetUserAsync(User);

            if (user != null)
            {
                await _signInService.SignOutAsync(user);
            }
            else
            {
                await _signInService.SignOutAsync();
            }

            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            _logger.LogInformation("User logged out.");

            if (LogoutId != null)
            {
                var request = await _interactionService.GetLogoutContextAsync(LogoutId);

                return this.Redirect(request?.PostLogoutRedirectUri);
            }

            return this.LocalRedirect(returnUrl);
        }
    }
}