// Filename: PayoutStrategy.cs
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
using eDoxa.Arena.Challenges.Domain.Strategies;

namespace eDoxa.Arena.Challenges.Api.Application.Strategies
{
    public sealed class PayoutStrategy : IPayoutStrategy
    {
        private const string FileName = "PayoutCharts.csv";

        private static ILookup<PayoutEntries, PayoutChart> PayoutCharts
        {
            get
            {
                using (var streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), $"Resources/{FileName}")))
                using (var csvReader = new CsvReader(streamReader))
                {
                    return csvReader.GetRecords<PayoutChart>()
                        .ToLookup(payoutChart => new PayoutEntries(payoutChart.PayoutEntries), payoutChart => payoutChart);
                }
            }
        }

        public IPayout GetPayout(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            var payoutRecords = PayoutCharts[payoutEntries].ToList();

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

        private class PayoutChart
        {
            public int PayoutEntries { get; set; }

            public int BucketSize { get; set; }

            public decimal PrizeFactor { get; set; }
        }
    }
}
