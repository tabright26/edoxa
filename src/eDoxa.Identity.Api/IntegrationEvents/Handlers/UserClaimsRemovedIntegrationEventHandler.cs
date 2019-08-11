// Filename: UserClaimsRemovedIntegrationEventHandler.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    internal sealed class UserClaimsRemovedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsRemovedIntegrationEvent>
    {
        private readonly UserManager _userManager;

        public UserClaimsRemovedIntegrationEventHandler(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserClaimsRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            foreach (var (type, value) in integrationEvent.Claims)
            {
                var claim = new Claim(type, value);

                await _userManager.RemoveClaimAsync(user, claim);
            }
        }
    }
}
