// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain.Misc;
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
        private readonly IChallengePayoutFactory _challengePayoutFactory;

        public CashierDbContextSeeder(
            CashierDbContext context,
            IBundlesService bundlesService,
            IAccountRepository accountRepository,
            IChallengeRepository challengeRepository,
            IWebHostEnvironment environment,
            ILogger<CashierDbContextSeeder> logger,
            IChallengePayoutFactory challengePayoutFactory
        ) : base(environment, logger)
        {
            _accountRepository = accountRepository;
            _challengeRepository = challengeRepository;
            _challengePayoutFactory = challengePayoutFactory;
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

            if (!_context.Challenges.Any())
            {
                var payoutEntries = new PayoutEntries(5);

                var entryFee = new EntryFee(0, Currency.Token);

                var strategy = _challengePayoutFactory.CreateInstance();

                var payout = strategy.GetPayout(payoutEntries, entryFee);

                var challengeId1 = ChallengeId.Parse("a3bc170c-de79-4144-baed-138c054bec5c");

                var challenge1 = new Challenge(challengeId1, entryFee, payout);

                var challengeId2 = ChallengeId.Parse("d61ea4b2-a753-437d-9c88-073d83ae4a1e");

                var challenge2 = new Challenge(challengeId2, entryFee, payout);

                var challengeId3 = ChallengeId.Parse("18523171-4aaa-434c-bad7-0dda1f6604b0");

                var challenge3 = new Challenge(challengeId3, entryFee, payout);

                var challengeId4 = ChallengeId.Parse("b341220e-a158-4503-9799-cf33658a3571");

                var challenge4 = new Challenge(challengeId4, entryFee, payout);

                var challengeId5 = ChallengeId.Parse("6a0658e9-4fd7-4857-9417-18513bb1486f");

                var challenge5 = new Challenge(challengeId5, entryFee, payout);

                _challengeRepository.Create(
                    new List<IChallenge>
                    {
                        challenge1,
                        challenge2,
                        challenge3,
                        challenge4,
                        challenge5
                    });

                await _challengeRepository.CommitAsync();

                Logger.LogInformation("The challenge's being populated.");
            }
            else
            {
                Logger.LogInformation("The challenge's already populated.");
            }
        }
    }
}
