// Filename: TransactionFailedIntegrationEventHandler.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class TransactionFailedIntegrationEventHandler : IIntegrationEventHandler<TransactionFailedIntegrationEvent>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionFailedIntegrationEventHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task HandleAsync(TransactionFailedIntegrationEvent integrationEvent)
        {
            var metadata = new TransactionMetadata(integrationEvent.Metadata);

            var transaction = await _transactionRepository.FindTransactionAsync(metadata);

            if (transaction != null)
            {
                transaction.MarkAsFailed();

                await _transactionRepository.CommitAsync();
            }
        }
    }
}
