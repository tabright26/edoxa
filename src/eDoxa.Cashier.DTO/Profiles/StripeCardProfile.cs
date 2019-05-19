﻿// Filename: StripeCardProfile.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using Stripe;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class StripeCardProfile : Profile
    {
        public StripeCardProfile()
        {
            this.CreateMap<Card, StripeCardDTO>()
                .ForMember(card => card.Id, config => config.MapFrom(card => card.Id))
                .ForMember(card => card.Brand, config => config.MapFrom(card => card.Brand))
                .ForMember(card => card.Name, config => config.MapFrom(card => card.Name))
                .ForMember(card => card.ExpMonth, config => config.MapFrom(card => card.ExpMonth))
                .ForMember(card => card.ExpYear, config => config.MapFrom(card => card.ExpYear))
                .ForMember(card => card.Last4, config => config.MapFrom(card => card.Last4));
        }
    }
}
