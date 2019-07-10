// Filename: PayoutConverter.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    internal sealed class PayoutConverter : IValueConverter<IPayout, PayoutViewModel>
    {
        [NotNull]
        public PayoutViewModel Convert([NotNull] IPayout payout, [NotNull] ResolutionContext context)
        {
            return new PayoutViewModel
            {
                PrizePool = new PrizePoolViewModel
                {
                    Currency = payout.Buckets.First().Prize.Currency.Name,
                    Amount = payout.Buckets.Sum(bucket => bucket.Size * bucket.Prize)
                },
                Buckets = payout.Buckets.Select(
                        bucketModel => new BucketViewModel
                        {
                            Size = bucketModel.Size,
                            Prize = bucketModel.Prize.Amount
                        }
                    )
                    .ToArray()
            };
        }
    }
}
