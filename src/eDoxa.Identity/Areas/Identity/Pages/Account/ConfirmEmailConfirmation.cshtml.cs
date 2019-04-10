// Filename: ConfirmEmailConfirmation.cshtml.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using eDoxa.Identity.Application.IntegrationEvents;
using eDoxa.Identity.Application.Services;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailConfirmationModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IIntegrationEventService _integrationEventService;

        public ConfirmEmailConfirmationModel(UserService userService, IIntegrationEventService integrationEventService)
        {
            _userService = userService;
            _integrationEventService = integrationEventService;
        }

        [BindProperty]
        public Guid UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet(Guid userId, string statusMessage = null)
        {
            UserId = userId;
            StatusMessage = statusMessage;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.FindByIdAsync(UserId.ToString());

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

            return this.RedirectToPage(
                new
                {
                    userId, StatusMessage
                }
            );
        }
    }
}