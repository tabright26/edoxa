// Filename: BucketListProfile.cs
// Date Created: 2019-05-20
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

using eDoxa.Arena.Domain;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class BucketListProfile : Profile
    {
        public BucketListProfile()
        {
            this.CreateMap<IEnumerable<Bucket>, BucketListDTO>().ForMember(list => list.Items, config => config.MapFrom(buckets => buckets.ToList()));
        }
    }
}
