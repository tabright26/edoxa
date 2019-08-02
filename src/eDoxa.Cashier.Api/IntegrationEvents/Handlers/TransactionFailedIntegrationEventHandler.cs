// Filename: TransactionFailedIntegrationEventHandler.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
            var transaction = await _transactionRepository.FindTransactionAsync(TransactionId.FromGuid(integrationEvent.TransactionId));

            transaction?.MarkAsFailed();

            await _transactionRepository.CommitAsync();
        }
    }
}
