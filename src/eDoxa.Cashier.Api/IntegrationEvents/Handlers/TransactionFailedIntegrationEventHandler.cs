// Filename: TransactionFailedIntegrationEventHandler.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class TransactionFailedIntegrationEventHandler : IIntegrationEventHandler<TransactionFailedIntegrationEvent>
    {
        private readonly ITransactionService _transactionService;

        public TransactionFailedIntegrationEventHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task HandleAsync(TransactionFailedIntegrationEvent integrationEvent)
        {
            var transaction = await _transactionService.FindTransactionAsync(integrationEvent.TransactionId);

            if (transaction != null)
            {
                await _transactionService.MarkTransactionAsFailedAsync(transaction);
            }
        }
    }
}
