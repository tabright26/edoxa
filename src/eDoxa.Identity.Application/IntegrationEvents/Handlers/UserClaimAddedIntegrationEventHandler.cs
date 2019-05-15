// Filename: UserClaimAddedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class UserClaimAddedIntegrationEventHandler : IIntegrationEventHandler<UserClaimAddedIntegrationEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserClaimAddedIntegrationEventHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserClaimAddedIntegrationEvent integrationEvent)
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