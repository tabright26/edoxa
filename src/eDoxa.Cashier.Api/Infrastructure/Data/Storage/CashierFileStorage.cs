// Filename: CashierFileStorage.cs
// Date Created: 2019-08-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public sealed class CashierFileStorage : ICashierFileStorage
    {
        private readonly CloudFileShare _share;

        public CashierFileStorage()
        {
            var storageCredentials = new StorageCredentials(
                "edoxadev",
                "KjHiR9rgn7tLkyKl4fK8xsAH6+YAgTqX8EyHdy+mIEFaGQTtVdAnS2jmVkfzynLFnBzjJOSyHu6WR44eqWbUXA=="
            );

            var storageAccount = new CloudStorageAccount(storageCredentials, false);

            var cloudBlobClient = storageAccount.CreateCloudFileClient();

            _share = cloudBlobClient.GetShareReference("cashier");
        }

        public async Task<ILookup<PayoutEntries, PayoutLevel>> GetChallengePayoutStructuresAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            // TODO: Rename this file reference to ChallengePayoutLevels.csv
            var file = rootDirectory.GetFileReference("ChallengePayouts.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        PayoutEntries = default(int),
                        BucketSize = default(int),
                        PrizeFactor = default(decimal)
                    }
                )
                .ToLookup(record => new PayoutEntries(record.PayoutEntries), record => new PayoutLevel(new BucketSize(record.BucketSize), record.PrizeFactor));
        }
    }
}
