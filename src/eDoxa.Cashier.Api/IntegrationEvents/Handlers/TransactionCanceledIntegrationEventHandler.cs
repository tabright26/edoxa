// Filename: TransactionCanceledIntegrationEventHandler.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class TransactionCanceledIntegrationEventHandler : IIntegrationEventHandler<TransactionCanceledIntegrationEvent>
    {
        private readonly ITransactionService _transactionService;

        public TransactionCanceledIntegrationEventHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task HandleAsync(TransactionCanceledIntegrationEvent integrationEvent)
        {
            var transaction = await _transactionService.FindTransactionAsync(integrationEvent.TransactionId);

            if (transaction != null)
            {
                await _transactionService.MaskTransactionAsCanceledAsync(transaction);
            }
        }
    }
}
