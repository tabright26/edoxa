// Filename: UserClaimAddedIntegrationEventHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Infrastructure.Models;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class UserClaimsAddedIntegrationEventHandler : IIntegrationEventHandler<UserClaimsAddedIntegrationEvent>
    {
        private readonly UserManager<UserModel> _userManager;

        public UserClaimsAddedIntegrationEventHandler(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserClaimsAddedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            foreach (var (type, value) in integrationEvent.Claims)
            {
                var claim = new Claim(type, value);

                var claims = await _userManager.GetClaimsAsync(user);

                if (!claims.Contains(claim))
                {
                    await _userManager.AddClaimAsync(user, claim);
                }
            }
        }
    }
}
