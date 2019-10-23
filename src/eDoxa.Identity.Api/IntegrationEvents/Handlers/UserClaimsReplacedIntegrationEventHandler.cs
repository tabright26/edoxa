// Filename: UserClaimsReplacedIntegrationEventHandler.cs
// Date Created: 2019-10-06
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
    public sealed class UserClaimsReplacedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsReplacedIntegrationEvent>
    {
        private readonly IUserManager _userManager;

        public UserClaimsReplacedIntegrationEventHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserClaimsReplacedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            for (var index = 0; index < integrationEvent.ClaimCount; index++)
            {
                var claim = integrationEvent.Claims.ElementAt(index);

                var newClaim = integrationEvent.NewClaims.ElementAt(index);

                await _userManager.ReplaceClaimAsync(user, new Claim(claim.Type, claim.Value), new Claim(newClaim.Type, newClaim.Value));
            }
        }
    }
}
