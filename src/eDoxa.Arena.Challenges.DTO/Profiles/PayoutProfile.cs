// Filename: PayoutProfile.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class PayoutProfile : Profile
    {
        public PayoutProfile()
        {
            this.CreateMap<Payout, PayoutDTO>().ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));
        }
    }
}
