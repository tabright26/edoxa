// Filename: PayoutStrategy.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Linq;

using CsvHelper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Strategies;

using IdentityServer4.Extensions;

namespace eDoxa.Cashier.Api.Application.Strategies
{
    public sealed class PayoutStrategy : IPayoutStrategy
    {
        private const string PayoutFilePath = "Infrastructure/Data/Storage/SourceFiles/Payouts.csv";

        private static ILookup<PayoutEntries, Record> Payouts
        {
            get
            {
                using (var streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), PayoutFilePath)))
                using (var csvReader = new CsvReader(streamReader))
                {
                    return csvReader.GetRecords<Record>().ToLookup(record => new PayoutEntries(record.PayoutEntries), record => record);
                }
            }
        }

        public IPayout GetPayout(PayoutEntries entries, EntryFee entryFee)
        {
            var records = Payouts[entries].ToList();

            if (records.IsNullOrEmpty())
            {
                throw new NotSupportedException($"Payout entries value ({entries}) is not supported.");
            }

            var prize = entryFee.GetLowestPrize();

            var buckets = new Buckets(records.Select(record => new Bucket(prize.ApplyFactor(record.PrizeFactor), new BucketSize(record.BucketSize))));

            return new Payout(buckets);
        }

        private sealed class Record
        {
            public int PayoutEntries { get; set; }

            public int BucketSize { get; set; }

            public decimal PrizeFactor { get; set; }
        }
    }
}
