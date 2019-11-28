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
        private readonly IUserManager _userManager;

        public UserClaimsAddedIntegrationEventHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserClaimsAddedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());
            
            var claims = await _userManager.GetClaimsAsync(user);

            foreach (var claim in integrationEvent.Claims.Where(claim => !claims.Any(securityClaim => securityClaim.Type == claim.Type && securityClaim.Value == claim.Value)))
            {
                await _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
            }
        }
    }
}
