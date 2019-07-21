// Filename: BucketViewModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Profiles
{
    public class BucketViewModelProfile : Profile
    {
        public BucketViewModelProfile()
        {
            this.CreateMap<Bucket, BucketViewModel>()
                .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size))
                .ForMember(bucket => bucket.Prize, config => config.MapFrom(bucket => bucket.Prize.Amount));
        }
    }
}
