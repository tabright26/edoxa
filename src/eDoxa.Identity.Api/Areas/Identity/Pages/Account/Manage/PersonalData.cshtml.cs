// Filename: PersonalData.cshtml.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(IUserService userService, ILogger<PersonalDataModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userService.GetUserId(User)}'.");
            }

            return this.Page();
        }
    }
}
