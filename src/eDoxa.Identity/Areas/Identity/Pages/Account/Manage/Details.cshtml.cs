// Filename: Details.cshtml.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using eDoxa.Identity.Application.IntegrationEvents;
using eDoxa.Identity.Application.Services;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Areas.Identity.Pages.Account.Manage
{
    public class DetailsModel : PageModel
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly UserService _userService;
        private readonly SignInService _signInService;

        public DetailsModel(IIntegrationEventService integrationEventService, UserService userService, SignInService signInService)
        {
            _integrationEventService = integrationEventService;
            _userService = userService;
            _signInService = signInService;
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Birth date")]
        public string BirthDate { get; set; }

        [Display(Name = "Gamertag")]
        public string Gamertag { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            var name = await _userService.GetNameAsync(user);
            var birthDate = await _userService.GetBirthDateAsync(user);
            var gamertag = await _userService.GetTagAsync(user);

            Name = name;
            BirthDate = birthDate;
            Gamertag = gamertag;

            var email = await _userService.GetEmailAsync(user);
            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                Email = email, PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userService.IsEmailConfirmedAsync(user);

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            var email = await _userService.GetEmailAsync(user);

            if (Input.Email != email)
            {
                var setEmailResult = await _userService.SetEmailAsync(user, Input.Email);

                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userService.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }

                await _integrationEventService.PublishAsync(new UserEmailChangedIntegrationEvent(user.Id, Input.Email));
            }

            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userService.SetPhoneNumberAsync(user, Input.PhoneNumber);

                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userService.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }

                await _integrationEventService.PublishAsync(new UserPhoneNumberChangedIntegrationEvent(user.Id, Input.PhoneNumber));
            }

            await _signInService.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            var userId = await _userService.GetUserIdAsync(user);
            var email = await _userService.GetEmailAsync(user);
            var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                null,
                new
                {
                    userId, code
                },
                Request.Scheme
            );

            await _integrationEventService.PublishAsync(
                new EmailSentIntegrationEvent(
                    email,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                )
            );

            StatusMessage = "Verification email sent. Please check your email.";
            return this.RedirectToPage();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
    }
}