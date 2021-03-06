﻿// Filename: FileStorage.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure.CsvHelper.Extensions;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public class FileStorage
    {
        private static Lazy<IImmutableSet<IChallenge>> LazyChallenges =>
            new Lazy<IImmutableSet<IChallenge>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Challenges.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
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
                        .Cast<IChallenge>()
                        .ToImmutableHashSet();
                });

        private static Lazy<ILookup<ChallengePayoutEntries, ChallengePayoutChart>> LazyChallengePayouts =>
            new Lazy<ILookup<ChallengePayoutEntries, ChallengePayoutChart>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/ChallengePayouts.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                PayoutEntries = default(int),
                                BucketSize = default(int),
                                PrizeFactor = default(decimal)
                            })
                        .ToLookup(
                            record => new ChallengePayoutEntries(record.PayoutEntries),
                            record => new ChallengePayoutChart(new ChallengePayoutBucketSize(record.BucketSize), record.PrizeFactor));
                });

        private static Lazy<IImmutableSet<UserId>> LazyUsers =>
            new Lazy<IImmutableSet<UserId>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/Users.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid)
                            })
                        .Select(record => UserId.FromGuid(record.Id))
                        .ToImmutableHashSet();
                });

        public static IImmutableSet<UserId> Users => LazyUsers.Value;

        public static ILookup<ChallengePayoutEntries, ChallengePayoutChart> ChallengePayouts => LazyChallengePayouts.Value;

        public static IImmutableSet<IChallenge> Challenges => LazyChallenges.Value;

        public IImmutableSet<UserId> GetUsers()
        {
            return LazyUsers.Value;
        }

        public ILookup<ChallengePayoutEntries, ChallengePayoutChart> GetChallengePayouts()
        {
            return LazyChallengePayouts.Value;
        }

        public IImmutableSet<IChallenge> GetChallenges()
        {
            return LazyChallenges.Value;
        }
    }
}
