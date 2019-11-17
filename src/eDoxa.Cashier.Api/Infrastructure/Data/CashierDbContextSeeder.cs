﻿// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using static eDoxa.Cashier.Api.Infrastructure.Data.Storage.FileStorage;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextSeeder : DbContextSeeder
    {
        private readonly CashierDbContext _context;
        private readonly IBundlesService _bundlesService;
        private readonly IAccountRepository _accountRepository;
        private readonly IChallengeRepository _challengeRepository;

        public CashierDbContextSeeder(
            CashierDbContext context,
            IBundlesService bundlesService,
            IAccountRepository accountRepository,
            IChallengeRepository challengeRepository,
            IHostingEnvironment environment,
            ILogger<CashierDbContextSeeder> logger
        ) : base(environment, logger)
        {
            _accountRepository = accountRepository;
            _challengeRepository = challengeRepository;
            _context = context;
            _bundlesService = bundlesService;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_context.Accounts.Any())
            {
                var adminAccount = new Account(UserId.FromGuid(AppAdmin.Id));

                foreach (var user in Users)
                {
                    if (user.Id == adminAccount.UserId)
                    {
                        var moneyAccount = new MoneyAccount(adminAccount);

                        moneyAccount.Deposit(Money.FiveHundred, _bundlesService.FetchDepositMoneyBundles()).MarkAsSucceded(); // 500

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 490

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 480

                        moneyAccount.Charge(new TransactionId(), Money.Five).MarkAsSucceded(); // 475

                        moneyAccount.Charge(new TransactionId(), Money.Fifty).MarkAsSucceded(); // 425

                        moneyAccount.Payout(Money.Twenty).MarkAsSucceded(); // 445

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 435

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 425

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 415

                        moneyAccount.Payout(Money.Twenty).MarkAsSucceded(); // 435

                        moneyAccount.Withdrawal(Money.OneHundred, _bundlesService.FetchWithdrawalMoneyBundles()).MarkAsSucceded(); // 335

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 325

                        moneyAccount.Charge(new TransactionId(), Money.Five).MarkAsSucceded(); // 320

                        moneyAccount.Charge(new TransactionId(), Money.Fifty).MarkAsSucceded(); // 270

                        moneyAccount.Charge(new TransactionId(), Money.Ten).MarkAsSucceded(); // 260

                        var tokenAccount = new TokenAccount(adminAccount);

                        tokenAccount.Deposit(Token.OneMillion, _bundlesService.FetchDepositTokenBundles()).MarkAsSucceded(); // 1000000

                        tokenAccount.Reward(Token.FiftyThousand).MarkAsSucceded(); // 1050000

                        tokenAccount.Charge(new TransactionId(), Token.FiftyThousand).MarkAsSucceded(); // 1000000

                        tokenAccount.Charge(new TransactionId(), Token.FiftyThousand).MarkAsSucceded(); // 950000

                        tokenAccount.Charge(new TransactionId(), Token.TwoHundredFiftyThousand).MarkAsSucceded(); // 700000

                        tokenAccount.Charge(new TransactionId(), Token.FiveHundredThousand).MarkAsSucceded(); // 200000

                        tokenAccount.Charge(new TransactionId(), Token.FiftyThousand).MarkAsSucceded(); // 150000

                        tokenAccount.Payout(Token.OneHundredThousand).MarkAsSucceded(); // 250000

                        tokenAccount.Reward(Token.FiftyThousand).MarkAsSucceded(); // 300000

                        tokenAccount.Charge(new TransactionId(), Token.OneHundredThousand).MarkAsSucceded(); // 200000

                        _accountRepository.Create(adminAccount);
                    }
                    else
                    {
                        var account = new Account(user.Id);

                        _accountRepository.Create(account);
                    }
                }

                await _accountRepository.CommitAsync();

                Logger.LogInformation("The user's account being populated.");
            }
            else
            {
                Logger.LogInformation("The user's account already populated.");
            }

            if (!_context.Challenges.Any())
            {
                _challengeRepository.Create(Challenges);

                await _challengeRepository.CommitAsync();

                Logger.LogInformation("The challenge's being populated.");
            }
            else
            {
                Logger.LogInformation("The challenge's already populated.");
            }
        }

        protected override async Task SeedProductionAsync()
        {
            if (!_context.Accounts.Any(account => account.UserId == UserId.FromGuid(AppAdmin.Id)))
            {
                var account = new Account(UserId.FromGuid(AppAdmin.Id));

                var moneyAccount = new MoneyAccount(account);

                moneyAccount.Deposit(Money.FiveHundred, _bundlesService.FetchDepositMoneyBundles()).MarkAsSucceded();

                var tokenAccount = new TokenAccount(account);

                tokenAccount.Deposit(Token.FiveMillions, _bundlesService.FetchDepositTokenBundles()).MarkAsSucceded();

                _accountRepository.Create(account);

                await _accountRepository.CommitAsync();

                Logger.LogInformation("The admin account being populated.");
            }
            else
            {
                Logger.LogInformation("The admin account already populated.");
            }
        }
    }
}
