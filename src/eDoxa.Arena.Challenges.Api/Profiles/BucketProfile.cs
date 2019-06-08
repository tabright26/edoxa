// Filename: BucketProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class BucketProfile : Profile
    {
        public BucketProfile()
        {
            this.CreateMap<Bucket, BucketViewModel>()
                .ForMember(bucket => bucket.Prize, config => config.MapFrom<decimal>(bucket => bucket.Prize))
                .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size));
        }
    }
}
