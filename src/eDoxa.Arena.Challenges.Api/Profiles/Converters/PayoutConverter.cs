// Filename: PayoutConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    public sealed class PayoutConverter : IValueConverter<ChallengeModel, PayoutViewModel>
    {
        [NotNull]
        public PayoutViewModel Convert([NotNull] ChallengeModel challenge, [NotNull] ResolutionContext context)
        {
            return new PayoutViewModel
            {
                PrizePool = new PrizePoolViewModel
                {
                    Type = CurrencyType.FromValue(challenge.Buckets.First().PrizeCurrency),
                    Amount = challenge.Buckets.Sum(bucket => bucket.Size * bucket.PrizeAmount)
                },
                Buckets = challenge.Buckets.Select(
                        bucket => new BucketViewModel
                        {
                            Size = bucket.Size,
                            Prize = bucket.PrizeAmount
                        }
                    )
                    .ToArray()
            };
        }
    }
}
