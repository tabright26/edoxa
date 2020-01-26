// Filename: LoginController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Domain.Services;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/login")]
    [ApiExplorerSettings(GroupName = "Login")]
    public sealed class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        private readonly IIdentityServerInteractionService _interactionService;

        public LoginController(IUserService userService, ISignInService signInService, IIdentityServerInteractionService interactionService)
        {
            _userService = userService;
            _signInService = signInService;
            _interactionService = interactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var context = await _interactionService.GetAuthorizationContextAsync(request.ReturnUrl);

            if (context != null)
            {
                var user = await _userService.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    var result = await _signInService.PasswordSignInAsync(
                        user,
                        request.Password,
                        request.RememberMe,
                        true);

                    if (result.Succeeded)
                    {
                        //var claimPrincipal = await _signInService.CreateUserPrincipalAsync(user);

                        //await HttpContext.SignInAsync(claimPrincipal);

                        return this.Ok(request.ReturnUrl);
                    }
                }
            }

            return this.Unauthorized();
        }
    }
}
