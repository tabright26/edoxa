// Filename: BucketModelProfile.cs
// Date Created: 2019-07-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Infrastructure.Models.Profiles
{
    public sealed class BucketModelProfile : Profile
    {
        public BucketModelProfile()
        {
            this.CreateMap<Bucket, BucketModel>()
                .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size))
                .ForMember(bucket => bucket.PrizeCurrency, config => config.MapFrom(bucket => bucket.Prize.Currency.Value))
                .ForMember(bucket => bucket.PrizeAmount, config => config.MapFrom(bucket => bucket.Prize.Amount));
        }
    }
}
