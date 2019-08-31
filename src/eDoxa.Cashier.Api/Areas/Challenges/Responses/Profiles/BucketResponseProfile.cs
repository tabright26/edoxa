// Filename: BucketResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Areas.Challenges.Responses.Profiles
{
    public class BucketResponseProfile : Profile
    {
        public BucketResponseProfile()
        {
            this.CreateMap<Bucket, BucketResponse>()
                .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size))
                .ForMember(bucket => bucket.Prize, config => config.MapFrom(bucket => bucket.Prize.Amount));
        }
    }
}
