// Filename: LogoutController.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Identity.Domain.Services;

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/logout")]
    [ApiExplorerSettings(GroupName = "Logout")]
    public sealed class LogoutController : ControllerBase
    {
        private readonly ISignInService _signInService;
        private readonly IEventService _eventService;
        private readonly IIdentityServerInteractionService _interactionService;

        public LogoutController(ISignInService signInService, IEventService eventService, IIdentityServerInteractionService interactionService)
        {
            _signInService = signInService;
            _eventService = eventService;
            _interactionService = interactionService;
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync([FromQuery] string? logoutId = null)
        {
            var context = await _interactionService.GetLogoutContextAsync(logoutId);

            await _interactionService.RevokeTokensForCurrentSessionAsync();

            if (User?.Identity.IsAuthenticated ?? false)
            {
                await _signInService.SignOutAsync();

                await _eventService.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            var token = new LogoutTokenDto
            {
                LogoutId = logoutId,
                ClientName = context?.ClientName ?? context?.ClientId,
                PostLogoutRedirectUri = context?.PostLogoutRedirectUri,
                SignOutIFrameUrl = context?.SignOutIFrameUrl,
                ShowSignoutPrompt = context?.ShowSignoutPrompt ?? false,
                AutomaticRedirectAfterSignOut = true
            };

            return this.Ok(token);
        }
    }
}
