// Filename: BucketProfile.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class BucketProfile : Profile
    {
        public BucketProfile()
        {
            this.CreateMap<Bucket, BucketDTO>()
                .ForMember(bucket => bucket.Size, config => config.MapFrom<int>(bucket => bucket.Size))
                .ForMember(bucket => bucket.Prize, config => config.MapFrom<decimal>(bucket => bucket.Prize));
        }
    }
}