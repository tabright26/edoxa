// Filename: StripePaymentMethodCardResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Payment.Responses;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripePaymentMethodCardResponseProfile : Profile
    {
        public StripePaymentMethodCardResponseProfile()
        {
            this.CreateMap<PaymentMethodCard, StripePaymentMethodCardResponse>()
                .ForMember(card => card.Brand, config => config.MapFrom(card => card.Brand))
                .ForMember(card => card.Country, config => config.MapFrom(card => card.Country))
                .ForMember(card => card.ExpMonth, config => config.MapFrom(card => card.ExpMonth))
                .ForMember(card => card.ExpYear, config => config.MapFrom(card => card.ExpYear))
                .ForMember(card => card.Last4, config => config.MapFrom(card => card.Last4));
        }
    }
}
