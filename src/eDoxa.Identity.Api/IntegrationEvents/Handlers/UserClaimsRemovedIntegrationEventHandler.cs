// Filename: UserClaimsRemovedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserClaimsRemovedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsRemovedIntegrationEvent>
    {
        private readonly IUserManager _userManager;

        public UserClaimsRemovedIntegrationEventHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserClaimsRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            foreach (var claim in integrationEvent.Claims)
            {
                await _userManager.RemoveClaimAsync(user, new Claim(claim.Type, claim.Value));
            }
        }
    }
}
