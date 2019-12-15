// Filename: StripePaymentMethodResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripePaymentMethodResponseProfile : Profile
    {
        public StripePaymentMethodResponseProfile()
        {
            this.CreateMap<PaymentMethod, StripePaymentMethodDto>()
                .ForMember(paymentMethod => paymentMethod.Id, config => config.MapFrom(paymentMethod => paymentMethod.Id))
                .ForMember(paymentMethod => paymentMethod.Type, config => config.MapFrom(paymentMethod => paymentMethod.Type))
                .ForMember(paymentMethod => paymentMethod.Card, config => config.MapFrom(paymentMethod => paymentMethod.Card));
        }
    }
}
