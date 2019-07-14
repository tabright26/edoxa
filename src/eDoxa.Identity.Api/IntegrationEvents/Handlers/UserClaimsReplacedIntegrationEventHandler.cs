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

using eDoxa.Identity.Infrastructure.Models;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserClaimsReplacedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsReplacedIntegrationEvent>
    {
        private readonly UserManager<UserModel> _userManager;

        public UserClaimsReplacedIntegrationEventHandler(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserClaimsReplacedIntegrationEvent integrationEvent)
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
