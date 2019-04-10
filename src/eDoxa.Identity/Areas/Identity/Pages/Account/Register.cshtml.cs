// Filename: Register.cshtml.cs
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
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using eDoxa.Identity.Application.IntegrationEvents;
using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInService _signInService;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly UserService _userSerivce;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            IIntegrationEventService integrationEventService,
            UserService userSerivce,
            SignInService signInService,
            ILogger<RegisterModel> logger)
        {
            _integrationEventService = integrationEventService;
            _userSerivce = userSerivce;
            _signInService = signInService;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public SelectList Years { get; set; }

        public SelectList Months { get; set; }

        public SelectList Days { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (_signInService.IsSignedIn(User))
            {
                return this.Redirect("~/Identity/Account/Manage/AccountDetails");
            }

            ReturnUrl = returnUrl;

            this.PopulateYearsDropDownList();

            this.PopulateMonthsDropDownList();

            this.PopulateDaysDropDownList();

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User(Input.Email, Input.FirstName, Input.LastName, Input.Year, (int) Input.Month, Input.Day, Input.Gamertag);

                var result = await _userSerivce.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userSerivce.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        null,
                        new
                        {
                            userId = user.Id, code
                        },
                        Request.Scheme
                    );

                    await _integrationEventService.PublishAsync(
                        new EmailSentIntegrationEvent(
                            Input.Email,
                            "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                        )
                    );

                    return this.RedirectToPage(
                        "./ConfirmEmailConfirmation",
                        new
                        {
                            UserId = user.Id
                        }
                    );
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
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
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Gamertag")]
            public string Gamertag { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public int Year { get; set; }

            [Required]
            public Month Month { get; set; }

            [Required]
            public int Day { get; set; }
        }
    }
}