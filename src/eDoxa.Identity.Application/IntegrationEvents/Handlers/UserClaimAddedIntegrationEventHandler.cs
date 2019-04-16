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

using eDoxa.Identity.Application.Services;
using eDoxa.ServiceBus;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class UserClaimAddedIntegrationEventHandler : IIntegrationEventHandler<UserClaimAddedIntegrationEvent>
    {
        private readonly UserService _userService;

        public UserClaimAddedIntegrationEventHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(UserClaimAddedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());

            var claim = new Claim(integrationEvent.Type, integrationEvent.Value);

            var claims = await _userService.GetClaimsAsync(user);

            if (!claims.Contains(claim))
            {
                await _userService.AddClaimAsync(user, claim);
            }
        }
    }
}