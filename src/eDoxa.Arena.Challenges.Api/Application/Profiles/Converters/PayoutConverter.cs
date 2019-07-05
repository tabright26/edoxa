// Filename: PayoutConverter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles.Converters
{
    internal sealed class PayoutConverter : IValueConverter<ICollection<BucketModel>, PayoutViewModel>
    {
        [NotNull]
        public PayoutViewModel Convert([NotNull] ICollection<BucketModel> bucketModels, [NotNull] ResolutionContext context)
        {
            return new PayoutViewModel
            {
                PrizePool = new PrizePoolViewModel
                {
                    Currency = Currency.FromValue(bucketModels.First().PrizeCurrency).Name,
                    Amount = bucketModels.Sum(bucketModel => bucketModel.Size * bucketModel.PrizeAmount)
                },
                Buckets = bucketModels.Select(
                        bucketModel => new BucketViewModel
                        {
                            Size = bucketModel.Size,
                            Prize = bucketModel.PrizeAmount
                        }
                    )
                    .ToArray()
            };
        }
    }
}
