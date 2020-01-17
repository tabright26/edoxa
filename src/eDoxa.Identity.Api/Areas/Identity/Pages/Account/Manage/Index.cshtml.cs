﻿// Filename: Index.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public IndexModel(IUserService userService, ISignInService signInService, IServiceBusPublisher serviceBusPublisher)
        {
            _userService = userService;
            _signInService = signInService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public string Username { get; set; }

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

            var userName = await _userService.GetUserNameAsync(user);
            var email = await _userService.GetEmailAsync(user);
            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber
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
                var setEmailResult = await _userService.UpdateEmailAsync(user, Input.Email);

                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userService.GetUserIdAsync(user);

                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            if (Input.PhoneNumber != phoneNumber)
            {
                var result = await _userService.UpdatePhoneNumberAsync(user, Input.PhoneNumber);

                if (!result.Succeeded)
                {
                    var userId = await _userService.GetUserIdAsync(user);

                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
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

            var code = await _userService.GenerateEmailConfirmationTokenAsync(user);

            await _serviceBusPublisher.PublishUserEmailConfirmationTokenGeneratedIntegrationEventAsync(user.Id.ConvertTo<UserId>(), code);

            StatusMessage = "Verification email sent. Please check your email.";

            return this.RedirectToPage();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }
    }
}
