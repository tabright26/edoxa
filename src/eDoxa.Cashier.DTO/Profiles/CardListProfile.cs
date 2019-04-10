// Filename: CardListProfile.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using Stripe;

namespace eDoxa.Cashier.DTO.Profiles
{
    public sealed class CardListProfile : Profile
    {
        public CardListProfile()
        {
            this.CreateMap<StripeList<Card>, CardListDTO>().ForMember(list => list.Items, config => config.MapFrom(list => list.Data));
        }
    }
}