// Filename: CardListProfile.cs
// Date Created: 2019-05-06
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

using Stripe;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class StripeCardListProfile : Profile
    {
        public StripeCardListProfile()
        {
            this.CreateMap<IEnumerable<Card>, StripeCardListDTO>().ForMember(list => list.Items, config => config.MapFrom(list => list.ToList()));
        }
    }
}