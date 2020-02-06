// Filename: StripeAccountResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;

using Stripe;

namespace eDoxa.Payment.Api.Profiles
{
    internal sealed class StripeProfile : Profile
    {
        public StripeProfile()
        {
            this.CreateMap<Customer, StripeCustomerDto>()
                .ForMember(customer => customer.DefaultPaymentMethodId, config => config.MapFrom(customer => customer.InvoiceSettings.DefaultPaymentMethodId));

            this.CreateMap<PaymentMethodCard, StripePaymentMethodCardDto>()
                .ForMember(card => card.Brand, config => config.MapFrom(card => card.Brand))
                .ForMember(card => card.Country, config => config.MapFrom(card => card.Country))
                .ForMember(card => card.ExpMonth, config => config.MapFrom(card => card.ExpMonth))
                .ForMember(card => card.ExpYear, config => config.MapFrom(card => card.ExpYear))
                .ForMember(card => card.Last4, config => config.MapFrom(card => card.Last4));

            this.CreateMap<PaymentMethod, StripePaymentMethodDto>()
                .ForMember(paymentMethod => paymentMethod.Id, config => config.MapFrom(paymentMethod => paymentMethod.Id))
                .ForMember(paymentMethod => paymentMethod.Type, config => config.MapFrom(paymentMethod => paymentMethod.Type))
                .ForMember(paymentMethod => paymentMethod.Card, config => config.MapFrom(paymentMethod => paymentMethod.Card));
        }
    }
}
