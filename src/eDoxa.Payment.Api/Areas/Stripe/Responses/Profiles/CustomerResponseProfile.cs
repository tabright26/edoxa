// Filename: CustomerResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses.Profiles
{
    internal sealed class CustomerResponseProfile : Profile
    {
        public CustomerResponseProfile()
        {
            this.CreateMap<Customer, CustomerResponse>()
                .ForMember(customer => customer.DefaultPaymentMethodId, config => config.MapFrom(customer => customer.InvoiceSettings.DefaultPaymentMethod.Id));
        }
    }
}
