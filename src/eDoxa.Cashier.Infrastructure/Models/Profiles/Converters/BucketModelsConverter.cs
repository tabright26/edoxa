// Filename: BucketModelsConverter.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Models.Profiles.Converters
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
