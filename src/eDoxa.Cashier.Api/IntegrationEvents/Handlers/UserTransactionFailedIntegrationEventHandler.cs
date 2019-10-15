// Filename: UserTransactionFailedIntegrationEventHandler.cs
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
    public sealed class UserTransactionFailedIntegrationEventHandler : IIntegrationEventHandler<UserTransactionFailedIntegrationEvent>
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
