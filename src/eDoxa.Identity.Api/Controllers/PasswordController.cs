// Filename: PasswordController.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/password")]
    [ApiExplorerSettings(GroupName = "Password")]
    public sealed class PasswordController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public PasswordController(IUserService userService, IServiceBusPublisher serviceBusPublisher)
        {
            _userService = userService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [HttpPost("forgot")]
        [SwaggerOperation("User's forgot password.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
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

                    await _serviceBusPublisher.PublishUserPasswordResetTokenGeneratedIntegrationEventAsync(user.Id.ConvertTo<UserId>(), code);
                }

                return this.Ok();
            }

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("reset")]
        [SwaggerOperation("User's password reset.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return this.Ok();
                }

                var result = await _userService.ResetPasswordAsync(user, request.Code, request.Password);

                if (result.Succeeded)
                {
                    return this.Ok();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
