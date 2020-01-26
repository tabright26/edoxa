// Filename: PasswordResetController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/password/reset")]
    [ApiExplorerSettings(GroupName = "Password")]
    public sealed class PasswordResetController : ControllerBase
    {
        private readonly IUserService _userService;

        public PasswordResetController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [SwaggerOperation("User's password reset.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] ResetPasswordRequest request)
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
