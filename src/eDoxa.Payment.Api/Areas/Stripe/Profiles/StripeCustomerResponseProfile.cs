// Filename: StripeCustomerResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripeCustomerResponseProfile : Profile
    {
        public StripeCustomerResponseProfile()
        {
            this.CreateMap<Customer, StripeCustomerDto>()
                .ForMember(customer => customer.DefaultPaymentMethodId, config => config.MapFrom(customer => customer.InvoiceSettings.DefaultPaymentMethodId));
        }
    }
}
