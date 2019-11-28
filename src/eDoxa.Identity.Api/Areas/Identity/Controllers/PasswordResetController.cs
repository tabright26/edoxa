// Filename: PasswordResetController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Requests;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/password/reset")]
    [ApiExplorerSettings(GroupName = "Password")]
    public sealed class PasswordResetController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public PasswordResetController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [SwaggerOperation("User's password reset.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] PasswordResetPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return this.Ok();
                }

                // BUG: Quick fix. Must be refactored. Related to the encoding.
                var code = request.Code.Replace(" ", "+");

                var result = await _userManager.ResetPasswordAsync(user, code, request.Password);

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
