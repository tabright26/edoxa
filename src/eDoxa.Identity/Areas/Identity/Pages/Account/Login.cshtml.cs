// Filename: Login.cshtml.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;
        private readonly SignInService _signInService;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(UserService userService, SignInService signInService, ILogger<LoginModel> logger)
        {
            _userService = userService;
            _signInService = signInService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (_signInService.IsSignedIn(User))
            {
                return this.Redirect("~/Identity/Account/Manage/AccountDetails");
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInService.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var userName = await _userService.GetUserNameAsync(Input.Email);

                if (userName != null)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInService.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, true);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return this.LocalRedirect(returnUrl);
                    }

                    if (result.RequiresTwoFactor)
                    {
                        return this.RedirectToPage(
                            "./LoginWith2fa",
                            new
                            {
                                ReturnUrl = returnUrl, Input.RememberMe
                            }
                        );
                    }

                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return this.RedirectToPage("./Lockout");
                    }

                    if (result.IsNotAllowed)
                    {
                        var user = await _userService.FindByNameAsync(userName);

                        if (user != null)
                        {
                            var userId = await _userService.GetUserIdAsync(user);

                            if (!await _userService.IsEmailConfirmedAsync(user))
                            {
                                return this.RedirectToPage(
                                    "./ConfirmEmailConfirmation",
                                    new
                                    {
                                        userId, statusMessage = "Error: You must have a confirmed email to log in."
                                    }
                                );
                            }
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return this.Page();
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
    }
}