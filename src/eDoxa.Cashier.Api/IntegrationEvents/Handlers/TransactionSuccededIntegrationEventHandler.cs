// Filename: TransactionSuccededIntegrationEventHandler.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class TransactionSuccededIntegrationEventHandler : IIntegrationEventHandler<TransactionSuccededIntegrationEvent>
    {
        private readonly ITransactionService _transactionService;

        public TransactionSuccededIntegrationEventHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task HandleAsync(TransactionSuccededIntegrationEvent integrationEvent)
        {
            var metadata = new TransactionMetadata(integrationEvent.Metadata);

            var transaction = await _transactionService.FindTransactionAsync(metadata);

            if (transaction != null)
            {
                await _transactionService.MarkTransactionAsSuccededAsync(transaction);
            }
        }
    }
}
