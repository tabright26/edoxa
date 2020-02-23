// Filename: UserWithdrawSucceededIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserWithdrawSucceededIntegrationEventHandler : IIntegrationEventHandler<UserWithdrawSucceededIntegrationEvent>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public UserWithdrawSucceededIntegrationEventHandler(IAccountService accountService, ILogger<UserWithdrawSucceededIntegrationEventHandler> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task HandleAsync(UserWithdrawSucceededIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            var transactionId = integrationEvent.Transaction.Id.ParseEntityId<TransactionId>();

            if (await _accountService.AccountExistsAsync(userId))
            {
                var account = await _accountService.FindAccountAsync(userId);

                var result = await _accountService.MarkAccountTransactionAsSucceededAsync(account, transactionId);

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
