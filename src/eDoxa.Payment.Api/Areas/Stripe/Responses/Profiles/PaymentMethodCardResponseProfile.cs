// Filename: CardResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses.Profiles
{
    internal sealed class PaymentMethodCardResponseProfile : Profile
    {
        public PaymentMethodCardResponseProfile()
        {
            this.CreateMap<PaymentMethodCard, PaymentMethodCardResponse>()
                .ForMember(card => card.Brand, config => config.MapFrom(card => card.Brand))
                .ForMember(card => card.Country, config => config.MapFrom(card => card.Country))
                .ForMember(card => card.ExpMonth, config => config.MapFrom(card => card.ExpMonth))
                .ForMember(card => card.ExpYear, config => config.MapFrom(card => card.ExpYear))
                .ForMember(card => card.Last4, config => config.MapFrom(card => card.Last4));
        }
    }
}
