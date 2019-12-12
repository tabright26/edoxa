// Filename: UserRoleAddedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserRoleAddedIntegrationEventHandler : IIntegrationEventHandler<UserRoleAddedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserRoleAddedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserRoleAddedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());

            if (!await _userService.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userService.AddToRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}
