// Filename: PaymentMethodResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses.Profiles
{
    internal sealed class PaymentMethodResponseProfile : Profile
    {
        public PaymentMethodResponseProfile()
        {
            this.CreateMap<PaymentMethod, PaymentMethodResponse>()
                .ForMember(paymentMethod => paymentMethod.Id, config => config.MapFrom(paymentMethod => paymentMethod.Id))
                .ForMember(paymentMethod => paymentMethod.Type, config => config.MapFrom(paymentMethod => paymentMethod.Type))
                .ForMember(paymentMethod => paymentMethod.PaymentMethodCard, config => config.MapFrom(paymentMethod => paymentMethod.Card));
        }
    }
}
