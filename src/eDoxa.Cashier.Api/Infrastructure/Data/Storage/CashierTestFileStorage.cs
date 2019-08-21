// Filename: CashierTestFileStorage.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public class CashierTestFileStorage : ICashierTestFileStorage
    {
        private readonly IPayoutFactory _payoutFactory;
        private readonly CloudFileShare _share;

        public CashierTestFileStorage()
        {
            var storageCredentials = new StorageCredentials(
                "edoxadev",
                "KjHiR9rgn7tLkyKl4fK8xsAH6+YAgTqX8EyHdy+mIEFaGQTtVdAnS2jmVkfzynLFnBzjJOSyHu6WR44eqWbUXA=="
            );

            var storageAccount = new CloudStorageAccount(storageCredentials, false);

            var cloudBlobClient = storageAccount.CreateCloudFileClient();

            _share = cloudBlobClient.GetShareReference("cashier");

            _payoutFactory = new PayoutFactory();
        }

        public async Task<IImmutableSet<User>> GetUsersAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("Users.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

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
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("Challenges.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

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
