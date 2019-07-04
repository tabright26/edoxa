// Filename: AccountService.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Cashier.Api.Application.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IIntegrationEventService _integrationEventService;

        public AccountService(IAccountRepository accountRepository, IIntegrationEventService integrationEventService)
        {
            _accountRepository = accountRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task WithdrawalAsync(string connectAccountId, UserId userId, Money money, CancellationToken cancellationToken = default)
        {
            var account = new AccountMoney(await _accountRepository.FindUserAccountAsync(userId));

            var transaction = account.Withdrawal(money);

            await _accountRepository.CommitAsync(cancellationToken);

            await _integrationEventService.PublishAsync(
                new WithdrawalProcessedIntegrationEvent(
                    transaction.Id,
                    transaction.Description.Text,
                    connectAccountId,
                    new Price(money).ToCents()
                )
            );
        }

        public async Task DepositAsync(string customerId, UserId userId, ICurrency currency, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepository.FindUserAccountAsync(userId);

            switch (currency)
            {
                case Money money:
                {
                    var moneyAccount = new AccountMoney(account);

                    await this.DepositAsync(customerId, moneyAccount, money, cancellationToken);

                    break;
                }

                case Token token:
                {
                    var tokenAccount = new AccountToken(account);

                    await this.DepositAsync(customerId, tokenAccount, token, cancellationToken);

                    break;
                }

                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private async Task DepositAsync<TCurrency>(
            string customerId,
            IAccount<TCurrency> account,
            TCurrency currency,
            CancellationToken cancellationToken = default
        )
        where TCurrency : Currency
        {
            var transaction = account.Deposit(currency);

            await _accountRepository.CommitAsync(cancellationToken);

            await _integrationEventService.PublishAsync(
                new DepositProcessedIntegrationEvent(transaction.Id, transaction.Description.Text, customerId, new Price(currency).ToCents())
            );
        }
    }
}
