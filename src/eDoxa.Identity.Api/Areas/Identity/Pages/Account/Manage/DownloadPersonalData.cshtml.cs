// Filename: DownloadPersonalData.cshtml.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Managers;
using eDoxa.Identity.Api.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly CustomUserManager _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;

        public DownloadPersonalDataModel(CustomUserManager userManager, ILogger<DownloadPersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalDataProps = typeof(User).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

            var personalData = personalDataProps.ToDictionary(p => p.Name, p => p.GetValue(user)?.ToString() ?? "null");

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");

            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }
    }
}
