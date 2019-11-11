// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IAccountService _accountService;

        public UserCreatedIntegrationEventHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            await _accountService.CreateAccountAsync(integrationEvent.UserId);
        }
    }
}
