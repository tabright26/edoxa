// Filename: ExternalLogin.cshtml.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInService _signInService;
        private readonly UserService _userService;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(SignInService signInService, UserService userService, ILogger<ExternalLoginModel> logger)
        {
            _signInService = signInService;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        public SelectList Years { get; set; }

        public SelectList Months { get; set; }

        public SelectList Days { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGetAsync()
        {
            if (_signInService.IsSignedIn(User))
            {
                return this.Redirect("~/Identity/Account/Manage/AccountDetails");
            }

            this.PopulateYearsDropDownList();

            this.PopulateMonthsDropDownList();

            this.PopulateDaysDropDownList();

            return this.Page();
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

            var properties = _signInService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

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

            var info = await _signInService.GetExternalLoginInfoAsync();

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
            var result = await _signInService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);

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
            returnUrl = returnUrl ?? Url.Content("~/");

            // Get the information about the user from the external login provider
            var info = await _signInService.GetExternalLoginInfoAsync();

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
                var user = new User(Input.Email, Input.FirstName, Input.LastName, Input.Year, (int) Input.Month, Input.Day, Input.Gamertag);

                var result = await _userService.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await _userService.AddLoginAsync(user, info);

                    if (result.Succeeded)
                    {
                        await _signInService.SignInAsync(user, false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
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

        private void PopulateYearsDropDownList(object selectedYear = null)
        {
            var years = new HashSet<int>();

            for (var index = DateTime.UtcNow.Year; index > DateTime.UtcNow.Year - 99; index--)
            {
                years.Add(index);
            }

            Years = new SelectList(years, selectedYear);
        }

        private void PopulateMonthsDropDownList(object selectedMonth = null)
        {
            var months = from Month month in Enum.GetValues(typeof(Month))
                         select new
                         {
                             Value = month, Text = $"{month.ToString()} ({(int) month})"
                         };

            Months = new SelectList(months, "Value", "Text", selectedMonth);
        }

        private void PopulateDaysDropDownList(object selectedDay = null)
        {
            var days = new HashSet<int>();

            for (var index = 1; index <= 31; index++)
            {
                days.Add(index);
            }

            Days = new SelectList(days, selectedDay);
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "Last name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Gamertag")]
            public string Gamertag { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            public int Year { get; set; }

            [Required]
            public Month Month { get; set; }

            [Required]
            public int Day { get; set; }
        }
    }
}