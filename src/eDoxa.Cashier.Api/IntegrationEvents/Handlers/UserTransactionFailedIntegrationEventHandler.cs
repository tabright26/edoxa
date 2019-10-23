// Filename: UserTransactionFailedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserTransactionFailedIntegrationEventHandler : IIntegrationEventHandler<UserTransactionFailedIntegrationEvent>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public UserTransactionFailedIntegrationEventHandler(
            ITransactionRepository transactionRepository,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<UserTransactionFailedIntegrationEventHandler> logger
        )
        {
            _transactionRepository = transactionRepository;
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(UserTransactionFailedIntegrationEvent integrationEvent)
        {
            try
            {
                var transaction = await _transactionRepository.FindTransactionAsync(TransactionId.FromGuid(integrationEvent.TransactionId));

                if (transaction == null)
                {
                    _logger.LogDebug("The transaction does not exist.");
                }
                else
                {
                    transaction.MarkAsFailed();

                    await _transactionRepository.CommitAsync();

                    await _serviceBusPublisher.PublishUserTransactionEmailSentIntegrationEventAsync(integrationEvent.UserId, transaction);
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Something wrong happened when the integration event of the user transaction failed.");
            }
        }
    }
}
