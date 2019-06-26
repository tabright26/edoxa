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
using System.Collections.Generic;
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
        private const string FileName = "PayoutChart.csv";

        private static readonly IEnumerable<string> Paths = new[] {"bin/Release/netcoreapp2.2/Data/", "bin/Debug/netcoreapp2.2/Data/", "Data/"};

        private static ILookup<PayoutEntries, PayoutRecord> PayoutRecords
        {
            get
            {
                foreach (var path in Paths)
                {
                    var combinePath = Path.Combine(Directory.GetCurrentDirectory(), path);

                    if (Directory.Exists(combinePath))
                    {
                        using (var reader = new StreamReader(combinePath + FileName))
                        using (var csv = new CsvReader(reader))
                        {
                            return csv.GetRecords<PayoutRecord>()
                                .ToLookup(payoutRecord => new PayoutEntries(payoutRecord.PayoutEntries), payoutRecord => payoutRecord);
                        }
                    }
                }

                throw new IOException(FileName);
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
