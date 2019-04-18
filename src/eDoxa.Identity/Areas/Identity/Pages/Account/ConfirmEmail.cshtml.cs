// Filename: ConfirmEmail.cshtml.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserService _userService;

        public ConfirmEmailModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync([CanBeNull] string userId, [CanBeNull] string code)
        {
            if (userId == null || code == null)
            {
                return this.RedirectToPage("/AccountDetails");
            }

            var user = await _userService.FindByIdAsync(userId);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userService.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return this.Page();
        }
    }
}