// Filename: CardListProfile.cs
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
    internal sealed class CardListProfile : Profile
    {
        public CardListProfile()
        {
            this.CreateMap<StripeList<Card>, CardListDTO>().ForMember(list => list.Items, config => config.MapFrom(list => list.Data));
        }
    }
}