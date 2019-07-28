// Filename: PayoutConverter.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class PayoutConverter : ITypeConverter<ICollection<BucketModel>, IPayout>
    {
        
        public IPayout Convert( ICollection<BucketModel> bucketModels,  IPayout destination,  ResolutionContext context)
        {
            return new Payout(
                new Buckets(
                    bucketModels.Select(
                        bucket => new Bucket(new Prize(bucket.PrizeAmount, Currency.FromValue(bucket.PrizeCurrency)), new BucketSize(bucket.Size))
                    )
                )
            );
        }
    }
}
