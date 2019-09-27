// Filename: CashierFileStorage.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Storage.Azure.File.Abstractions;
using eDoxa.Storage.Azure.File.Extensions;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Storage
{
    public sealed class CashierFileStorage : ICashierFileStorage
    {
        private readonly IAzureFileStorage _fileStorage;

        public CashierFileStorage(IAzureFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<ILookup<PayoutEntries, PayoutLevel>> GetChallengePayoutStructuresAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var file = await root.GetFileAsync("ChallengePayoutLevels.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        PayoutEntries = default(int),
                        BucketSize = default(int),
                        PrizeFactor = default(decimal)
                    })
                .ToLookup(record => new PayoutEntries(record.PayoutEntries), record => new PayoutLevel(new BucketSize(record.BucketSize), record.PrizeFactor));
        }
    }
}
