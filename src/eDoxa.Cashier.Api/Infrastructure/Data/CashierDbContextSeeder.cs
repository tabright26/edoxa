// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IAccountRepository _accountRepository;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IChallengePayoutFactory _challengePayoutFactory;

        public CashierDbContextSeeder(
            CashierDbContext context,
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
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_context.Accounts.Any())
            {
                var adminAccount = new Account(UserId.FromGuid(AppAdmin.Id));

                foreach (var userId in Users)
                {
                    if (userId == adminAccount.Id)
                    {
                        var moneyAccount = new MoneyAccountDecorator(adminAccount);

                        moneyAccount.Deposit(Money.FiveHundred).MarkAsSucceeded(); // 500

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 490

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 480

                        moneyAccount.Charge(Money.Five).MarkAsSucceeded(); // 475

                        moneyAccount.Charge(Money.Fifty).MarkAsSucceeded(); // 425

                        moneyAccount.Payout(Money.Twenty).MarkAsSucceeded(); // 445

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 435

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 425

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 415

                        moneyAccount.Payout(Money.Twenty).MarkAsSucceeded(); // 435

                        moneyAccount.Withdrawal(Money.OneHundred).MarkAsSucceeded(); // 335

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 325

                        moneyAccount.Charge(Money.Five).MarkAsSucceeded(); // 320

                        moneyAccount.Charge(Money.Fifty).MarkAsSucceeded(); // 270

                        moneyAccount.Charge(Money.Ten).MarkAsSucceeded(); // 260

                        var tokenAccount = new TokenAccountDecorator(adminAccount);

                        tokenAccount.Deposit(Token.OneMillion).MarkAsSucceeded(); // 1000000

                        tokenAccount.Reward(Token.FiftyThousand).MarkAsSucceeded(); // 1050000

                        tokenAccount.Charge(Token.FiftyThousand).MarkAsSucceeded(); // 1000000

                        tokenAccount.Charge(Token.FiftyThousand).MarkAsSucceeded(); // 950000

                        tokenAccount.Charge(Token.TwoHundredFiftyThousand).MarkAsSucceeded(); // 700000

                        tokenAccount.Charge(Token.FiveHundredThousand).MarkAsSucceeded(); // 200000

                        tokenAccount.Charge(Token.FiftyThousand).MarkAsSucceeded(); // 150000

                        tokenAccount.Payout(Token.OneHundredThousand).MarkAsSucceeded(); // 250000

                        tokenAccount.Reward(Token.FiftyThousand).MarkAsSucceeded(); // 300000

                        tokenAccount.Charge(Token.OneHundredThousand).MarkAsSucceeded(); // 200000

                        _accountRepository.Create(adminAccount);
                    }
                    else
                    {
                        var account = new Account(userId);

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
            if (!_context.Accounts.Any(account => account.Id == UserId.FromGuid(AppAdmin.Id)))
            {
                var account = new Account(UserId.FromGuid(AppAdmin.Id));

                var moneyAccount = new MoneyAccountDecorator(account);

                moneyAccount.Deposit(Money.FiveHundred).MarkAsSucceeded();

                var tokenAccount = new TokenAccountDecorator(account);

                tokenAccount.Deposit(Token.FiveMillions).MarkAsSucceeded();

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
                var entryFee = new EntryFee(0, Currency.Token);

                var strategy = _challengePayoutFactory.CreateInstance();

                var payoutForTwoEntries = strategy.GetPayout(PayoutEntries.One, entryFee);

                var challengeId1WithTwoEntries = ChallengeId.Parse("675fd61f-50a7-4268-8ed3-790428dd94c6");

                var challenge1WithTwoEntries = new Challenge(challengeId1WithTwoEntries, entryFee, payoutForTwoEntries);

                var challengeId2WithTwoEntries = ChallengeId.Parse("3e7326e7-f2b0-4da6-92fb-60aad19b7aff");

                var challenge2WithTwoEntries = new Challenge(challengeId2WithTwoEntries, entryFee, payoutForTwoEntries);

                var challengeId3WithTwoEntries = ChallengeId.Parse("c653e421-5439-4016-82d7-013a494a3eb0");

                var challenge3WithTwoEntries = new Challenge(challengeId3WithTwoEntries, entryFee, payoutForTwoEntries);

                var challengeId4WithTwoEntries = ChallengeId.Parse("0923e7c5-413b-47ec-a98b-4c97d2534acf");

                var challenge4WithTwoEntries = new Challenge(challengeId4WithTwoEntries, entryFee, payoutForTwoEntries);

                var challengeId5WithTwoEntries = ChallengeId.Parse("92c4c94f-a1f6-485d-b4bb-5555d5974419");

                var challenge5WithTwoEntries = new Challenge(challengeId5WithTwoEntries, entryFee, payoutForTwoEntries);

                var payoutForFourEntries = strategy.GetPayout(PayoutEntries.Two, entryFee);

                var challengeId1WithFourEntries = ChallengeId.Parse("effc77f4-0961-4c3c-873b-b88abb1e97f2");

                var challenge1WithFourEntries = new Challenge(challengeId1WithFourEntries, entryFee, payoutForFourEntries);

                var challengeId2WithFourEntries = ChallengeId.Parse("7e5290e0-f11b-4409-bcb5-211d115e33ee");

                var challenge2WithFourEntries = new Challenge(challengeId2WithFourEntries, entryFee, payoutForFourEntries);

                var challengeId3WithFourEntries = ChallengeId.Parse("fb59ae64-771c-4cd7-b555-ec4bb14c69bf");

                var challenge3WithFourEntries = new Challenge(challengeId3WithFourEntries, entryFee, payoutForFourEntries);

                var challengeId4WithFourEntries = ChallengeId.Parse("086d982a-fff6-493c-9294-eefba208ebd8");

                var challenge4WithFourEntries = new Challenge(challengeId4WithFourEntries, entryFee, payoutForFourEntries);

                var challengeId5WithFourEntries = ChallengeId.Parse("2bd0dfc8-576f-4d5b-851c-5a914e186e2c");

                var challenge5WithFourEntries = new Challenge(challengeId5WithFourEntries, entryFee, payoutForFourEntries);

                var payoutForSixEntries = strategy.GetPayout(PayoutEntries.Three, entryFee);

                var challengeId1WithSixEntries = ChallengeId.Parse("4d15b0c6-d53d-4f75-a2ba-17c713c79677");

                var challenge1WithSixEntries = new Challenge(challengeId1WithSixEntries, entryFee, payoutForSixEntries);

                var challengeId2WithSixEntries = ChallengeId.Parse("a8c93b67-6174-468f-8265-be9ca078bc96");

                var challenge2WithSixEntries = new Challenge(challengeId2WithSixEntries, entryFee, payoutForSixEntries);

                _challengeRepository.Create(
                    new List<IChallenge>
                    {
                        challenge1WithTwoEntries,
                        challenge2WithTwoEntries,
                        challenge3WithTwoEntries,
                        challenge4WithTwoEntries,
                        challenge5WithTwoEntries,
                        challenge1WithFourEntries,
                        challenge2WithFourEntries,
                        challenge3WithFourEntries,
                        challenge4WithFourEntries,
                        challenge5WithFourEntries,
                        challenge1WithSixEntries,
                        challenge2WithSixEntries
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
