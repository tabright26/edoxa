// Filename: Logout.cshtml.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        //private readonly CustomSignInManager _signInManager;
        //private readonly ILogger<LogoutModel> _logger;
        //private readonly IIdentityServerInteractionService _identityServerInteractionService;

        //public LogoutModel(CustomSignInManager signInManager, ILogger<LogoutModel> logger, IIdentityServerInteractionService identityServerInteractionService)
        //{
        //    _signInManager = signInManager;
        //    _logger = logger;
        //    _identityServerInteractionService = identityServerInteractionService;
        //}

        public IActionResult OnGet(string returnUrl = null)
        {
            return this.RedirectToAction(
                "Logout",
                "Account",
                new
                {
                    area = "",
                    returnUrl
                }
            );
        }

        //public async Task<IActionResult> OnPost(string returnUrl = null)
        //{
        //    await _identityServerInteractionService.RevokeTokensForCurrentSessionAsync();

        //    await _signInManager.SignOutAsync();

        //    _logger.LogInformation("User logged out.");

        //    if (returnUrl != null)
        //    {
        //        return this.LocalRedirect(returnUrl);
        //    }

        //    return this.RedirectToPage();
        //}
    }
}
