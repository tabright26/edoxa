// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using static eDoxa.Cashier.Api.Infrastructure.Data.Storage.FileStorage;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextSeeder : DbContextSeeder
    {
        private readonly IChallengePayoutFactory _challengePayoutFactory;

        public CashierDbContextSeeder(
            CashierDbContext context,
            IChallengePayoutFactory challengePayoutFactory,
            IWebHostEnvironment environment,
            ILogger<CashierDbContextSeeder> logger
        ) : base(context, environment, logger)
        {
            _challengePayoutFactory = challengePayoutFactory;
            Accounts = context.Set<AccountModel>();
            ChallengePayouts = context.Set<ChallengePayoutModel>();
            Promotions = context.Set<PromotionModel>();
        }

        private DbSet<AccountModel> Accounts { get; }

        private DbSet<ChallengePayoutModel> ChallengePayouts { get; }

        private DbSet<PromotionModel> Promotions { get; }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!await Promotions.AnyAsync())
            {
                var duration = TimeSpan.FromDays(365);

                var promotion = new Promotion("TEST", Money.Five, duration, new DateTimeProvider(DateTime.UtcNow + duration));

                Promotions.Add(promotion.ToModel());

                await this.CommitAsync();

                Logger.LogInformation("The admin account being populated.");
            }
            else
            {
                Logger.LogInformation("The admin account already populated.");
            }

            if (!await Accounts.AnyAsync())
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

                        Accounts.Add(adminAccount.ToModel());
                    }
                    else
                    {
                        var account = new Account(userId);

                        Accounts.Add(account.ToModel());
                    }
                }

                await this.CommitAsync();

                Logger.LogInformation("The user's account being populated.");
            }
            else
            {
                Logger.LogInformation("The user's account already populated.");
            }

            if (!await ChallengePayouts.AnyAsync())
            {
                ChallengePayouts.AddRange(Challenges.Select(challengePayout => challengePayout.ToModel()));

                await this.CommitAsync();

                Logger.LogInformation("The challenge's being populated.");
            }
            else
            {
                Logger.LogInformation("The challenge's already populated.");
            }
        }

        protected override async Task SeedProductionAsync()
        {
            if (!await Promotions.AnyAsync())
            {
                var duration = TimeSpan.FromDays(365);

                var promotion = new Promotion("TEST", Money.Five, duration, new DateTimeProvider(DateTime.UtcNow + duration));

                Promotions.Add(promotion.ToModel());

                await this.CommitAsync();

                Logger.LogInformation("The admin account being populated.");
            }
            else
            {
                Logger.LogInformation("The admin account already populated.");
            }

            if (!await Accounts.AnyAsync(account => account.Id == UserId.FromGuid(AppAdmin.Id)))
            {
                var account = new Account(UserId.FromGuid(AppAdmin.Id));

                var moneyAccount = new MoneyAccountDecorator(account);

                moneyAccount.Deposit(Money.FiveHundred).MarkAsSucceeded();

                var tokenAccount = new TokenAccountDecorator(account);

                tokenAccount.Deposit(Token.FiveMillions).MarkAsSucceeded();

                Accounts.Add(account.ToModel());

                await this.CommitAsync();

                Logger.LogInformation("The admin account being populated.");
            }
            else
            {
                Logger.LogInformation("The admin account already populated.");
            }

            var strategy = _challengePayoutFactory.CreateInstance();

            var twoDollars = new EntryFee(2, CurrencyType.Money);

            var threeDollars = new EntryFee(3, CurrencyType.Money);

            var twoDollarsForOneEntries = strategy.GetChallengePayout(ChallengePayoutEntries.One, twoDollars);

            var twoDollarsForTwoEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Two, twoDollars);

            var twoDollarsForThreeEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Three, twoDollars);

            var threeDollarsForOneEntries = strategy.GetChallengePayout(ChallengePayoutEntries.One, threeDollars);

            var threeDollarsForTwoEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Two, threeDollars);

            var threeDollarsForThreeEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Three, threeDollars);

            var challengePayouts = new List<IChallenge>
            {
                new Challenge(ChallengeId.Parse("d53b366f-e717-43d4-ac12-6e13d37f5cef"), twoDollarsForOneEntries),
                new Challenge(ChallengeId.Parse("369ae69d-b10d-4d72-84ba-698691646ba6"), twoDollarsForOneEntries),
                new Challenge(ChallengeId.Parse("eb76fa60-700f-4dce-b312-d69897563437"), twoDollarsForTwoEntries),
                new Challenge(ChallengeId.Parse("82592581-e6ac-41e0-9c61-773d924f233d"), twoDollarsForTwoEntries),
                new Challenge(ChallengeId.Parse("9457ae9a-4e5c-436f-b10f-33134af68439"), twoDollarsForThreeEntries),
                new Challenge(ChallengeId.Parse("91f6d007-b458-4f1c-9814-755b32059e00"), twoDollarsForThreeEntries),
                new Challenge(ChallengeId.Parse("4ecb13a4-0742-4140-93b0-27ee582e5cab"), threeDollarsForOneEntries),
                new Challenge(ChallengeId.Parse("fa38f697-2ef3-40e9-a165-d62c3cc750a8"), threeDollarsForOneEntries),
                new Challenge(ChallengeId.Parse("ac6851b4-2cb7-42ab-bf44-fb197d21221b"), threeDollarsForTwoEntries),
                new Challenge(ChallengeId.Parse("bb5f6e0c-ada7-47b4-9d24-a3c9ec7df034"), threeDollarsForTwoEntries),
                new Challenge(ChallengeId.Parse("6ec217f7-3d6a-41c2-b2eb-4cc8799d2af5"), threeDollarsForThreeEntries),
                new Challenge(ChallengeId.Parse("7d96b314-8d5b-4393-9257-9c0e2cf7c0f1"), threeDollarsForThreeEntries)
            };

            ChallengePayouts.AddRange(
                challengePayouts.Where(challengePayout => ChallengePayouts.All(x => x.ChallengeId != challengePayout.Id))
                    .Select(challengePayout => challengePayout.ToModel()));

            await this.CommitAsync();
        }
    }
}
