// Filename: StripePaymentMethodResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Payment.Responses;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripePaymentMethodResponseProfile : Profile
    {
        public StripePaymentMethodResponseProfile()
        {
            this.CreateMap<PaymentMethod, StripePaymentMethodResponse>()
                .ForMember(paymentMethod => paymentMethod.Id, config => config.MapFrom(paymentMethod => paymentMethod.Id))
                .ForMember(paymentMethod => paymentMethod.Type, config => config.MapFrom(paymentMethod => paymentMethod.Type))
                .ForMember(paymentMethod => paymentMethod.StripePaymentMethodCard, config => config.MapFrom(paymentMethod => paymentMethod.Card));
        }
    }
}
