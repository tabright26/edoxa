// Filename: PersonalData.cshtml.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.IdentityServer.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly CustomUserManager _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(CustomUserManager userManager, ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return this.Page();
        }
    }
}
