// Filename: UserClaimsReplacedIntegrationEventHandler.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    internal sealed class UserClaimsReplacedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsReplacedIntegrationEvent>
    {
        private readonly CustomUserManager _userManager;

        public UserClaimsReplacedIntegrationEventHandler(CustomUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserClaimsReplacedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            for (var index = 0; index < integrationEvent.ClaimCount; index++)
            {
                var (type, value) = integrationEvent.Claims.ElementAt(index);

                var claim = new Claim(type, value);

                var (newType, newValue) = integrationEvent.NewClaims.ElementAt(index);

                var newClaim = new Claim(newType, newValue);

                await _userManager.ReplaceClaimAsync(user, claim, newClaim);
            }
        }
    }
}
