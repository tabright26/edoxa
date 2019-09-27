// Filename: CashierTestFileStorage.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Storage.Azure.File.Abstractions;
using eDoxa.Storage.Azure.File.Extensions;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public class CashierTestFileStorage : ICashierTestFileStorage
    {
        private readonly IPayoutFactory _payoutFactory = new PayoutFactory();
        private readonly IAzureFileStorage _fileStorage;

        public CashierTestFileStorage(IAzureFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<IImmutableSet<User>> GetUsersAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("Users.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        Id = default(Guid)
                    }
                )
                .Select(record => new User(UserId.FromGuid(record.Id)))
                .ToImmutableHashSet();
        }

        public async Task<IImmutableSet<IChallenge>> GetChallengesAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("Challenges.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        Id = default(Guid),
                        EntryFeeCurrency = default(int),
                        EntryFeeAmount = default(decimal),
                        PayoutEntries = default(int)
                    }
                )
                .Select(
                    record =>
                    {
                        var payoutStrategy = _payoutFactory.CreateInstance();

                        var payoutEntries = new PayoutEntries(record.PayoutEntries);

                        var currency = Currency.FromValue(record.EntryFeeCurrency)!;

                        var entryFee = new EntryFee(record.EntryFeeAmount, currency);

                        var payout = payoutStrategy.GetPayoutAsync(payoutEntries, entryFee).Result;

                        var challenge = new Challenge(entryFee, payout);

                        challenge.SetEntityId(ChallengeId.FromGuid(record.Id));

                        return challenge;
                    }
                )
                .Cast<IChallenge>()
                .ToImmutableHashSet();
        }
    }
}
