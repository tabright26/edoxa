// Filename: EmailConfirmController.cs
// Date Created: 2019-08-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/email/confirm")]
    [ApiExplorerSettings(GroupName = "Email")]
    public class EmailConfirmController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public EmailConfirmController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        ///     User's forgot password.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string? userId, [FromQuery] string? code)
        {
            if (userId != null && code != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{userId}'.");
                }

                // BUG: Quick fix. Must be refactored. Related to the encoding.
                code = code.Replace(" ", "+");

                var result = await _userManager.ConfirmEmailAsync(user, code);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
                }
            }

            return this.Ok();
        }
    }
}
