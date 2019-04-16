// Filename: UserClaimRemovedIntegrationEventHandler.cs
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
    public class UserClaimRemovedIntegrationEventHandler : IIntegrationEventHandler<UserClaimRemovedIntegrationEvent>
    {
        private readonly UserService _userService;

        public UserClaimRemovedIntegrationEventHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(UserClaimRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());

            var claim = new Claim(integrationEvent.Type, integrationEvent.Value);

            await _userService.RemoveClaimAsync(user, claim);
        }
    }
}