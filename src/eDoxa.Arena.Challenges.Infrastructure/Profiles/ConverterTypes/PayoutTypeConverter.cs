// Filename: PayoutTypeConverter.cs
// Date Created: 2019-06-22
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class PayoutTypeConverter : ITypeConverter<ICollection<BucketModel>, IPayout>
    {
        [NotNull]
        public IPayout Convert([NotNull] ICollection<BucketModel> source, [NotNull] IPayout destination, [NotNull] ResolutionContext context)
        {
            return new Payout(
                new Buckets(
                    source.Select(
                        bucket => new Bucket(new Prize(bucket.PrizeAmount, Currency.FromValue(bucket.PrizeCurrency)), new BucketSize(bucket.Size))
                    )
                )
            );
        }
    }
}
