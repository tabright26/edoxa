// Filename: UserRoleAddedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserRoleAddedIntegrationEventHandler : IIntegrationEventHandler<UserRoleAddedIntegrationEvent>
    {
        private readonly IUserManager _userManager;

        public UserRoleAddedIntegrationEventHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserRoleAddedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            if (!await _userManager.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userManager.AddToRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}
