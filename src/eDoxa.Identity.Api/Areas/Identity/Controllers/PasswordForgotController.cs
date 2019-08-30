// Filename: PasswordForgotController.cs
// Date Created: 2019-08-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;
using System.Web;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/password/forgot")]
    [ApiExplorerSettings(GroupName = "Password")]
    public sealed class PasswordForgotController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IRedirectService _redirectService;

        public PasswordForgotController(IUserManager userManager, IEmailSender emailSender, IRedirectService redirectService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _redirectService = redirectService;
        }

        /// <summary>
        ///     User's forgot password.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PasswordForgotPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.Ok();
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = $"{_redirectService.RedirectToWebSpa("/security/password/reset")}?code={code}";

                await _emailSender.SendEmailAsync(
                    request.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HttpUtility.UrlEncode(callbackUrl)}'>clicking here</a>."
                );

                return this.Ok();
            }

            return this.BadRequest(ModelState);
        }
    }
}
