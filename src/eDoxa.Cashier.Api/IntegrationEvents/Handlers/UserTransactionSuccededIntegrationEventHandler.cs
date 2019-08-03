// Filename: UserAccountTransactionSuccededIntegrationEventHandler.cs
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
    internal sealed class UserTransactionSuccededIntegrationEventHandler : IIntegrationEventHandler<UserTransactionSuccededIntegrationEvent>
    {
        private readonly ITransactionRepository _transactionRepository;

        public UserTransactionSuccededIntegrationEventHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task HandleAsync(UserTransactionSuccededIntegrationEvent integrationEvent)
        {
            var transaction = await _transactionRepository.FindTransactionAsync(TransactionId.FromGuid(integrationEvent.TransactionId));

            transaction?.MarkAsSucceded();

            await _transactionRepository.CommitAsync();
        }
    }
}
