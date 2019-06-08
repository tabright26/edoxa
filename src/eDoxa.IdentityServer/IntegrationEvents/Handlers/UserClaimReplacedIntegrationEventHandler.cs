// Filename: UserClaimReplacedIntegrationEventHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.IdentityServer.IntegrationEvents.Handlers
{
    public sealed class UserClaimReplacedIntegrationEventHandler : IIntegrationEventHandler<UserClaimReplacedIntegrationEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserClaimReplacedIntegrationEventHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserClaimReplacedIntegrationEvent integrationEvent)
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
