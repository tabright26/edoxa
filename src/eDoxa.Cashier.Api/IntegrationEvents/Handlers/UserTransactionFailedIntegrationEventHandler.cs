// Filename: UserTransactionFailedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserTransactionFailedIntegrationEventHandler : IIntegrationEventHandler<UserTransactionFailedIntegrationEvent>
    {
        private readonly IAccountService _accountService;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public UserTransactionFailedIntegrationEventHandler(
            IAccountService accountService,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<UserTransactionFailedIntegrationEventHandler> logger
        )
        {
            _accountService = accountService;
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(UserTransactionFailedIntegrationEvent integrationEvent)
        {
            var userId = UserId.Parse(integrationEvent.UserId);

            var account = await _accountService.FindAccountAsync(userId);

            if (account != null)
            {
                var result = await _accountService.MarkAccountTransactionAsFailedAsync(account, TransactionId.Parse(integrationEvent.TransactionId));

                if (result.IsValid)
                {
                    await _serviceBusPublisher.PublishUserTransactionEmailSentIntegrationEventAsync(userId, result.GetEntityFromMetadata<ITransaction>());
                }
                else
                {
                    _logger.LogCritical("Something wrong happened when the user transaction integration event was successful.");
                }
            }
        }
    }
}
