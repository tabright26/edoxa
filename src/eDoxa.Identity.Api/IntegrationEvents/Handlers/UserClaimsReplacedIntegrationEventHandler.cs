// Filename: UserClaimsReplacedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserClaimsReplacedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsReplacedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserClaimsReplacedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserClaimsReplacedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());

            for (var index = 0; index < integrationEvent.ClaimCount; index++)
            {
                var claim = integrationEvent.Claims.ElementAt(index);

                var newClaim = integrationEvent.NewClaims.ElementAt(index);

                await _userService.ReplaceClaimAsync(user, new Claim(claim.Type, claim.Value), new Claim(newClaim.Type, newClaim.Value));
            }
        }
    }
}
