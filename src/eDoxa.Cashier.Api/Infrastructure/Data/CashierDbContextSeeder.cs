// Filename: CashierDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions;

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
            Challenges = context.Set<ChallengeModel>();
            Promotions = context.Set<PromotionModel>();
        }

        private DbSet<AccountModel> Accounts { get; }

        private DbSet<ChallengeModel> Challenges { get; }

        private DbSet<PromotionModel> Promotions { get; }

        protected override async Task SeedDevelopmentAsync()
        {
            await this.SeedTestPromotionsAsync();

            await this.SeedTestAccountsAsync();

            await this.SeedTestChallengesAsync();
        }

        protected override async Task SeedStagingAsync()
        {
            await this.SeedTestPromotionsAsync();

            await this.SeedAdministratorAccountAsync();

            await this.SeedChallengesAsync();
        }

        protected override async Task SeedProductionAsync()
        {
            await this.SeedAdministratorAccountAsync();

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

            var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Challenges.Production.csv"));

            using var csvReader = file.OpenCsvReader();

            Challenges.AddRange(
                csvReader.GetRecords(
                        new
                        {
                            Id = default(Guid),
                            EntryFeeCurrency = default(int),
                            EntryFeeAmount = default(decimal),
                            PayoutEntries = default(int)
                        })
                    .Select(
                        record =>
                        {
                            var payoutStrategy = new ChallengePayoutFactory().CreateInstance();

                            var payoutEntries = new ChallengePayoutEntries(record.PayoutEntries);

                            var currency = CurrencyType.FromValue(record.EntryFeeCurrency)!;

                            var entryFee = new EntryFee(record.EntryFeeAmount, currency);

                            var payout = payoutStrategy.GetChallengePayout(payoutEntries, entryFee);

                            return new Challenge(record.Id.ConvertTo<ChallengeId>(), payout);
                        })
                    .Where(challenge => Challenges.All(x => x.Id != challenge.Id))
                    .Select(challenge => challenge.ToModel()));

            await this.CommitAsync();

            var startedAt = DateTimeOffset.FromUnixTimeMilliseconds(1581656400000).UtcDateTime;

            var duration = TimeSpan.FromDays(3);

            var promotion1 = new Promotion(
                "LANETS20REDCUP",
                new Money(5),
                duration,
                new DateTimeProvider(startedAt + duration));

            promotion1.SetEntityId(PromotionId.Parse("ff5cd605-0209-4f5d-8dec-88673286416c"));

            var promotion2 = new Promotion(
                "LANETS20TOK",
                new Token(250),
                duration,
                new DateTimeProvider(startedAt + duration));

            promotion2.SetEntityId(PromotionId.Parse("313ff1de-2432-40d3-b9e3-f319a001a979"));

            var promotions = new List<Promotion>
            {
                promotion1,
                promotion2
            };

            Promotions.AddRange(promotions.Where(promotion => Promotions.All(x => x.Id != promotion.Id)).Select(promotion => promotion.ToModel()));

            await this.CommitAsync();
        }

        private async Task SeedTestPromotionsAsync()
        {
            if (!await Promotions.AnyAsync())
            {
                var duration = TimeSpan.FromDays(365 * 100);

                var promotions = new List<Promotion>
                {
                    new Promotion(
                        "TWOHUNDREDFIFTYTOKENS",
                        new Token(250),
                        duration,
                        new DateTimeProvider(DateTime.UtcNow + duration)),
                    new Promotion(
                        "FIVEDOLLARS",
                        new Money(5),
                        duration,
                        new DateTimeProvider(DateTime.UtcNow + duration))
                };

                Promotions.AddRange(promotions.Select(promotion => promotion.ToModel()));

                await this.CommitAsync();

                Logger.LogInformation("The promotions being populated.");
            }
            else
            {
                Logger.LogInformation("The promotions already populated.");
            }
        }

        private async Task SeedTestAccountsAsync()
        {
            if (!await Accounts.AnyAsync())
            {
                foreach (var userId in Users)
                {
                    if (userId == AppAdministrator.Id)
                    {
                        var account = Account.CreateTestAdministrator(userId);

                        Accounts.Add(account.ToModel());
                    }
                    else
                    {
                        var account = new Account(userId);

                        Accounts.Add(account.ToModel());
                    }
                }

                await this.CommitAsync();

                Logger.LogInformation("The users account being populated.");
            }
            else
            {
                Logger.LogInformation("The users account already populated.");
            }
        }

        private async Task SeedAdministratorAccountAsync()
        {
            var userId = UserId.FromGuid(AppAdministrator.Id);

            if (!await Accounts.AnyAsync(account => account.Id == userId))
            {
                var account = new Account(userId);

                Accounts.Add(account.ToModel());

                await this.CommitAsync();

                Logger.LogInformation("The administrator account being populated.");
            }
            else
            {
                Logger.LogInformation("The administrator account already populated.");
            }
        }

        private async Task SeedTestChallengesAsync()
        {
            if (!await Challenges.AnyAsync())
            {
                Challenges.AddRange(FileStorage.Challenges.Select(challengePayout => challengePayout.ToModel()));

                await this.CommitAsync();

                Logger.LogInformation("The challenges being populated.");
            }
            else
            {
                Logger.LogInformation("The challenges already populated.");
            }
        }

        private async Task SeedChallengesAsync()
        {
            var strategy = _challengePayoutFactory.CreateInstance();

            var twoDollars = new EntryFee(2, CurrencyType.Money);

            var threeDollars = new EntryFee(3, CurrencyType.Money);

            var twoDollarsForOneEntries = strategy.GetChallengePayout(ChallengePayoutEntries.One, twoDollars);

            var twoDollarsForTwoEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Two, twoDollars);

            var twoDollarsForThreeEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Three, twoDollars);

            var threeDollarsForOneEntries = strategy.GetChallengePayout(ChallengePayoutEntries.One, threeDollars);

            var threeDollarsForTwoEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Two, threeDollars);

            var threeDollarsForThreeEntries = strategy.GetChallengePayout(ChallengePayoutEntries.Three, threeDollars);

            var challenges = new List<IChallenge>
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

            Challenges.AddRange(challenges.Where(challenge => Challenges.All(x => x.Id != challenge.Id)).Select(challenge => challenge.ToModel()));

            await this.CommitAsync();
        }
    }
}
