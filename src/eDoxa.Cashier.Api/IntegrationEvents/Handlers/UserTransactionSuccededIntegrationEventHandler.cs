// Filename: UserTransactionSuccededIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserTransactionSuccededIntegrationEventHandler : IIntegrationEventHandler<UserTransactionSuccededIntegrationEvent>
    {
        private readonly IAccountService _accountService;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public UserTransactionSuccededIntegrationEventHandler(
            IAccountService accountService,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<UserTransactionSuccededIntegrationEventHandler> logger
        )
        {
            _accountService = accountService;
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(UserTransactionSuccededIntegrationEvent integrationEvent)
        {
            var account = await _accountService.FindAccountAsync(integrationEvent.UserId);

            if (account != null)
            {
                var result = await _accountService.MarkAccountTransactionAsSuccededAsync(account, integrationEvent.TransactionId);

                if (result.IsValid)
                {
                    await _serviceBusPublisher.PublishUserTransactionEmailSentIntegrationEventAsync(integrationEvent.UserId, result.GetEntityFromMetadata<ITransaction>());
                }
                else
                {
                    _logger.LogCritical("Something wrong happened when the user transaction integration event was successful.");
                }
            }
        }
    }
}
