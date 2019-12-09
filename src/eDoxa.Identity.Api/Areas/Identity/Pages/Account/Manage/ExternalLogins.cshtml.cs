// Filename: ExternalLogins.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account.Manage
{
    public class ExternalLoginsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;

        public ExternalLoginsModel(IUserService userService, ISignInService signInService)
        {
            _userService = userService;
            _signInService = signInService;
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            CurrentLogins = await _userService.GetLoginsAsync(user);

            OtherLogins = (await _signInService.GetExternalAuthenticationSchemesAsync()).Where(auth => CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();

            ShowRemoveButton = user.PasswordHash != null || CurrentLogins.Count > 1;

            return this.Page();
        }

        public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            var result = await _userService.RemoveLoginAsync(user, loginProvider, providerKey);

            if (!result.Succeeded)
            {
                var userId = await _userService.GetUserIdAsync(user);

                throw new InvalidOperationException($"Unexpected error occurred removing external login for user with ID '{userId}'.");
            }

            await _signInService.RefreshSignInAsync(user);
            StatusMessage = "The external login was removed.";

            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostLinkLoginAsync(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("./ExternalLogins", "LinkLoginCallback");
            var properties = _signInService.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userService.GetUserId(User));

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            var info = await _signInService.GetExternalLoginInfoAsync(await _userService.GetUserIdAsync(user));

            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userService.AddLoginAsync(user, info);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";

            return this.RedirectToPage();
        }
    }
}
