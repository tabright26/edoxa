// Filename: ConfirmEmail.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly IUserService _userService;

        public ConfirmEmailModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string? userId, string? code)
        {
            if (userId == null || code == null)
            {
                return this.RedirectToPage("/Index");
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
