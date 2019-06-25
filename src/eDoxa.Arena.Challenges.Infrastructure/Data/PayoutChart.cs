// Filename: PayoutChart.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;
using System.Linq;

using Castle.Core.Internal;

using CsvHelper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Infrastructure.Data
{
    public sealed class PayoutChart
    {
        private static ILookup<PayoutEntries, PayoutRecord> PayoutRecords
        {
            get
            {
                using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"Data\PayoutChart.csv")))
                using (var csv = new CsvReader(reader))
                {
                    return csv.GetRecords<PayoutRecord>().ToLookup(payoutRecord => new PayoutEntries(payoutRecord.PayoutEntries), payoutRecord => payoutRecord);
                }
            }
        }

        public IPayout GetPayout(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            var payoutRecords = PayoutRecords[payoutEntries].ToList();

            if (payoutRecords.IsNullOrEmpty())
            {
                throw new NotSupportedException($"Payout entries value ({payoutEntries}) is not supported.");
            }

            var prize = entryFee.GetLowestPrize();

            return new Payout(
                new Buckets(
                    payoutRecords.Select(payoutRecord => new Bucket(prize.ApplyFactor(payoutRecord.PrizeFactor), new BucketSize(payoutRecord.BucketSize)))
                )
            );
        }

        private class PayoutRecord
        {
            public int PayoutEntries { get; set; }

            public int BucketSize { get; set; }

            public decimal PrizeFactor { get; set; }
        }
    }
}
