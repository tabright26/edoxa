﻿// Filename: Disable2fa.cshtml.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserService _userService;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(UserService userService, ILogger<Disable2faModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            if (!await _userService.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"Cannot disable 2FA for user with ID '{_userService.GetUserId(User)}' as it's not currently enabled.");
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            var disable2faResult = await _userService.SetTwoFactorEnabledAsync(user, false);

            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for user with ID '{_userService.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userService.GetUserId(User));
            StatusMessage = "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";
            return this.RedirectToPage("./TwoFactorAuthentication");
        }
    }
}