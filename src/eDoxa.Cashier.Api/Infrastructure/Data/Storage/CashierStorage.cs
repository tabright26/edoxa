// Filename: CashierStorage.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public static class CashierStorage
    {
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";
        private const string TestChallengesFilePath = "Infrastructure/Data/Storage/TestFiles/TestChallenges.csv";

        public static IReadOnlyCollection<IChallenge> TestChallenges => GetTestChallenges().ToList();

        public static IReadOnlyCollection<User> TestUsers => GetTestUsers().ToList();

        public static User TestAdmin => GetTestUsers().ToList().First();

        private static IEnumerable<User> GetTestUsers()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUsersFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords(
                        new
                        {
                            Id = default(Guid)
                        }
                    ).ToList();

                foreach (var record in records)
                {
                    yield return new User(UserId.FromGuid(record.Id));
                }
            }
        }

        private static IEnumerable<IChallenge> GetTestChallenges()
        {
            var payoutFactory = new PayoutFactory();
            var payoutStrategy = payoutFactory.CreateInstance();
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestChallengesFilePath);

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords(
                        new
                        {
                            Id = default(Guid),
                            EntryFeeCurrency = default(int),
                            EntryFeeAmount = default(decimal),
                            PayoutEntries = default(int)
                        }
                    )
                    .ToList();

                foreach (var record in records)
                {
                    var payoutEntries = new PayoutEntries(record.PayoutEntries);

                    var currency = Currency.FromValue(record.EntryFeeCurrency);

                    var entryFee = new EntryFee(record.EntryFeeAmount, currency);

                    var payout = payoutStrategy.GetPayout(payoutEntries, entryFee);

                    var challenge = new Challenge(entryFee, payout);

                    challenge.SetEntityId(ChallengeId.FromGuid(record.Id));

                    yield return challenge;
                }
            }
        }
    }
}
