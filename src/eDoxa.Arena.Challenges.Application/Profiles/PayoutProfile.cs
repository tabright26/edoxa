// Filename: PayoutProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Application.Profiles
{
    internal sealed class PayoutProfile : Profile
    {
        public PayoutProfile()
        {
            this.CreateMap<Payout, PayoutViewModel>()
                .ForMember(payout => payout.Currency, config => config.MapFrom(payout => payout.Currency))
                .ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));
        }
    }
}
