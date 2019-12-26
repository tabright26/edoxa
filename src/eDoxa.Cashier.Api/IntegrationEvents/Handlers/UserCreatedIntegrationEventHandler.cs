// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public UserCreatedIntegrationEventHandler(IAccountService accountService, ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (!await _accountService.AccountExistsAsync(userId))
            {
                var result = await _accountService.CreateAccountAsync(userId);

                if (result.IsValid)
                {
                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogError(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogWarning(""); // FRANCIS: TODO.
            }
        }
    }
}
