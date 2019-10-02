// Filename: FileStorage.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

using eDoxa.Cashier.Api.Areas.Challenges.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public class FileStorage
    {
        private static Lazy<IImmutableSet<IChallenge>> LazyChallenges =>
            new Lazy<IImmutableSet<IChallenge>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/challenges.csv"));

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
                                var payoutStrategy = new PayoutFactory().CreateInstance();

                                var payoutEntries = new PayoutEntries(record.PayoutEntries);

                                var currency = Currency.FromValue(record.EntryFeeCurrency)!;

                                var entryFee = new EntryFee(record.EntryFeeAmount, currency);

                                var payout = payoutStrategy.GetPayout(payoutEntries, entryFee);

                                var challenge = new Challenge(entryFee, payout);

                                challenge.SetEntityId(ChallengeId.FromGuid(record.Id));

                                return challenge;
                            })
                        .Cast<IChallenge>()
                        .ToImmutableHashSet();
                });

        private static Lazy<ILookup<PayoutEntries, PayoutLevel>> LazyChallengePayouts =>
            new Lazy<ILookup<PayoutEntries, PayoutLevel>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/challenges.payouts.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                PayoutEntries = default(int),
                                BucketSize = default(int),
                                PrizeFactor = default(decimal)
                            })
                        .ToLookup(
                            record => new PayoutEntries(record.PayoutEntries),
                            record => new PayoutLevel(new BucketSize(record.BucketSize), record.PrizeFactor));
                });

        private static Lazy<IImmutableSet<User>> LazyUsers =>
            new Lazy<IImmutableSet<User>>(
                () =>
                {
                    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var file = File.OpenRead(Path.Combine(assemblyPath, "Setup/users.csv"));

                    using var csvReader = file.OpenCsvReader();

                    return csvReader.GetRecords(
                            new
                            {
                                Id = default(Guid)
                            })
                        .Select(record => new User(UserId.FromGuid(record.Id)))
                        .ToImmutableHashSet();
                });

        public static IImmutableSet<User> Users => LazyUsers.Value;

        public static ILookup<PayoutEntries, PayoutLevel> ChallengePayouts => LazyChallengePayouts.Value;

        public static IImmutableSet<IChallenge> Challenges => LazyChallenges.Value;

        public IImmutableSet<User> GetUsers()
        {
            return LazyUsers.Value;
        }

        public ILookup<PayoutEntries, PayoutLevel> GetChallengePayouts()
        {
            return LazyChallengePayouts.Value;
        }

        public IImmutableSet<IChallenge> GetChallenges()
        {
            return LazyChallenges.Value;
        }
    }
}
