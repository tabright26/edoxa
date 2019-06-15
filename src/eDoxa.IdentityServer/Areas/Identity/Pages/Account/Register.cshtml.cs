// Filename: Register.cshtml.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.IdentityServer.IntegrationEvents;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace eDoxa.IdentityServer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly IEventBusService _eventBusService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IEventBusService eventBusService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _eventBusService = eventBusService;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; } =
            BirthDate.Years().Select(year => new SelectListItem(year.ToString(), year.ToString())).OrderByDescending(item => item.Value);

        public IEnumerable<SelectListItem> Months { get; set; } =
            BirthDate.Months().Select(month => new SelectListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(month), month.ToString()));

        public IEnumerable<SelectListItem> Days { get; set; } = BirthDate.Days().Select(day => new SelectListItem(day.ToString(), day.ToString()));

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var personalName = new PersonalName(Input.FirstName, Input.LastName);
                var birthDate = new BirthDate(Input.Year, Input.Month, Input.Day);
                var user = new User(Input.UserName, Input.Email, personalName, birthDate);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    _eventBusService.Publish(
                        new UserCreatedIntegrationEvent(
                            user.Id,
                            user.Email,
                            personalName.FirstName,
                            personalName.LastName,
                            birthDate.Year,
                            birthDate.Month,
                            birthDate.Day
                        )
                    );

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        null,
                        new
                        {
                            userId = user.Id,
                            code
                        },
                        Request.Scheme
                    );

                    await _emailSender.SendEmailAsync(
                        Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                    );

                    await _signInManager.SignInAsync(user, false);

                    return this.LocalRedirect(returnUrl);
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
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // TODO: Must be a valid date.
            [Required] public int Year { get; set; }

            // TODO: Must be a valid date.
            [Required] public int Month { get; set; }

            // TODO: Must be a valid date.
            [Required] public int Day { get; set; }

            [RegularExpression("(True|true)", ErrorMessage = "You must accept the terms of service.")]
            public bool TermsOfService { get; set; }
        }
    }
}
