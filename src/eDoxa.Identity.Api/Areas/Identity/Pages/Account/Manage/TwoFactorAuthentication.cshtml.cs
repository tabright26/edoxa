// Filename: TwoFactorAuthentication.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Threading.Tasks;

using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        //private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}";

        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        //private readonly ILogger<TwoFactorAuthenticationModel> _logger;

        public TwoFactorAuthenticationModel(IUserService userService, ISignInService signInService/*, ILogger<TwoFactorAuthenticationModel> logger*/)
        {
            _userService = userService;
            _signInService = signInService;
            //_logger = logger;
        }

        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        [BindProperty]
        public bool Is2faEnabled { get; set; }

        public bool IsMachineRemembered { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            HasAuthenticator = await _userService.GetAuthenticatorKeyAsync(user) != null;
            Is2faEnabled = await _userService.GetTwoFactorEnabledAsync(user);
            IsMachineRemembered = await _signInService.IsTwoFactorClientRememberedAsync(user);
            RecoveryCodesLeft = await _userService.CountRecoveryCodesAsync(user);

            return this.Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            await _signInService.ForgetTwoFactorClientAsync();
            StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";

            return this.RedirectToPage();
        }
    }
}
