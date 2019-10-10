// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IAccountRepository _accountRepository;

        public UserCreatedIntegrationEventHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var account = new Account(UserId.FromGuid(integrationEvent.UserId));

            _accountRepository.Create(account);

            await _accountRepository.CommitAsync();
        }
    }
}
