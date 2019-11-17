// Filename: PasswordForgotController.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;
using System.Web;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Authorization;
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
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IRedirectService _redirectService;

        public PasswordForgotController(IUserManager userManager, IServiceBusPublisher serviceBusPublisher, IRedirectService redirectService)
        {
            _userManager = userManager;
            _serviceBusPublisher = serviceBusPublisher;
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

                // Don't reveal that the user does not exist or is not confirmed
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var callbackUrl = $"{_redirectService.RedirectToWebSpaProxy("/password/reset")}?code={HttpUtility.UrlEncode(code)}";

                    await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
                        UserId.FromGuid(user.Id),
                        request.Email,
                        "Reset Password",
                        $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");
                }

                return this.Ok();
            }

            return this.BadRequest(ModelState);
        }
    }
}
