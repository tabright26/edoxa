// Filename: UserClaimsAddedIntegrationEventHandler.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserClaimsAddedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsAddedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserClaimsAddedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserClaimsAddedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());
            
            var claims = await _userService.GetClaimsAsync(user);

            foreach (var claim in integrationEvent.Claims.Where(claim => !claims.Any(securityClaim => securityClaim.Type == claim.Type && securityClaim.Value == claim.Value)))
            {
                await _userService.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
            }
        }
    }
}
