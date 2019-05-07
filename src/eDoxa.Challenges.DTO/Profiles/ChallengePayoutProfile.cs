// Filename: ChallengePayoutProfile.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.Entities;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengePayoutProfile : Profile
    {
        public ChallengePayoutProfile()
        {
            this.CreateMap<Payout, ChallengePayoutDTO>()
                .ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));
        }
    }
}