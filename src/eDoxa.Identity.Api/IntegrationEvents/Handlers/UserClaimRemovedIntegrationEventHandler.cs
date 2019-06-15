// Filename: UserClaimRemovedIntegrationEventHandler.cs
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

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class UserClaimRemovedIntegrationEventHandler : IIntegrationEventHandler<UserClaimRemovedIntegrationEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserClaimRemovedIntegrationEventHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserClaimRemovedIntegrationEvent integrationEvent)
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
