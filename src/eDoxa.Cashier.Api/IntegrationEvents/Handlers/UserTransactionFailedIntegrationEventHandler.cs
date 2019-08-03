// Filename: UserAccountTransactionFailedIntegrationEventHandler.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    internal sealed class UserTransactionFailedIntegrationEventHandler : IIntegrationEventHandler<UserTransactionFailedIntegrationEvent>
    {
        private readonly ITransactionRepository _transactionRepository;

        public UserTransactionFailedIntegrationEventHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task HandleAsync(UserTransactionFailedIntegrationEvent integrationEvent)
        {
            var transaction = await _transactionRepository.FindTransactionAsync(TransactionId.FromGuid(integrationEvent.TransactionId));

            transaction?.MarkAsFailed();

            await _transactionRepository.CommitAsync();
        }
    }
}
