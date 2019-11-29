// Filename: PasswordForgotController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;
using System.Web;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Requests;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/password/forgot")]
    [ApiExplorerSettings(GroupName = "Password")]
    public sealed class PasswordForgotController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IRedirectService _redirectService;

        public PasswordForgotController(IUserService userService, IServiceBusPublisher serviceBusPublisher, IRedirectService redirectService)
        {
            _userService = userService;
            _serviceBusPublisher = serviceBusPublisher;
            _redirectService = redirectService;
        }

        [HttpPost]
        [SwaggerOperation("User's forgot password.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByEmailAsync(request.Email);

                // Don't reveal that the user does not exist or is not confirmed
                if (user != null && await _userService.IsEmailConfirmedAsync(user))
                {
                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userService.GeneratePasswordResetTokenAsync(user);

                    var callbackUrl = $"{_redirectService.RedirectToWebSpa("/password/reset")}?code={HttpUtility.UrlEncode(code)}";

                    await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
                        UserId.FromGuid(user.Id),
                        request.Email,
                        "Reset Password",
                        $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");
                }

                return this.Ok();
            }

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
