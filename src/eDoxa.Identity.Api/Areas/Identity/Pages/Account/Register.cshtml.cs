// Filename: Register.cshtml.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

#nullable disable

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly IRedirectService _redirectService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;

        public RegisterModel(
            UserManager userManager,
            SignInManager signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRedirectService redirectService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _redirectService = redirectService;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= _redirectService.RedirectToWebSpa();

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(
                    new User
                    {
                        Email = Input.Email,
                        UserName = Input.Email
                    },
                    Input.Password
                );

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl =
                        $"{_redirectService.RedirectToWebSpa("/email/confirm")}?userId={user.Id}&code={code}";

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                    );

                    await _signInManager.SignInAsync(user, false);

                    return this.Redirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
