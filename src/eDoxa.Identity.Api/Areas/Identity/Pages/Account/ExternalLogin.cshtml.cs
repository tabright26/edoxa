// Filename: ExternalLogin.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;

        public ExternalLoginModel(SignInManager signInManager, UserManager userManager, ILogger<ExternalLoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData] public string ErrorMessage { get; set; }

        public IActionResult OnGetAsync()
        {
            return this.RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page(
                "./ExternalLogin",
                "Callback",
                new
                {
                    returnUrl
                }
            );

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");

            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";

                return this.RedirectToPage(
                    "./Login",
                    new
                    {
                        ReturnUrl = returnUrl
                    }
                );
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";

                return this.RedirectToPage(
                    "./Login",
                    new
                    {
                        ReturnUrl = returnUrl
                    }
                );
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);

                return this.LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return this.RedirectToPage("./Lockout");
            }

            // If the user does not have an account, then ask the user to create an account.
            ReturnUrl = returnUrl;
            LoginProvider = info.LoginProvider;

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                Input = new InputModel
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                };
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";

                return this.RedirectToPage(
                    "./Login",
                    new
                    {
                        ReturnUrl = returnUrl
                    }
                );
            }

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Email = Input.Email,
                    UserName = Input.Email,
                    Country = Input.Country
                });

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);

                    result = await _userManager.AddLoginAsync(user, info);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        await _signInManager.SignInAsync(user, false);

                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            LoginProvider = info.LoginProvider;
            ReturnUrl = returnUrl;

            return this.Page();
        }

        public class InputModel
        {
            [Required] [EmailAddress]
            public string Email { get; set; }

            public Country Country { get; set; } = Country.Canada;
        }
    }
}
