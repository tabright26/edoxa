// Filename: UserTransactionSuccededIntegrationEventHandler.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserTransactionSuccededIntegrationEventHandler : IIntegrationEventHandler<UserTransactionSuccededIntegrationEvent>
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
