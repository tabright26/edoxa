// Filename: BucketModelsConverter.cs
// Date Created: 2019-06-21
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

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class BucketModelsConverter : IValueConverter<IPayout, ICollection<BucketModel>>
    {
        [NotNull]
        public ICollection<BucketModel> Convert([NotNull] IPayout sourceMember, [NotNull] ResolutionContext context)
        {
            return sourceMember.Buckets.Select(
                    bucket => new BucketModel
                    {
                        Size = bucket.Size,
                        PrizeCurrency = bucket.Prize.Currency.Value,
                        PrizeAmount = bucket.Prize.Amount
                    }
                )
                .ToList();
        }
    }
}
