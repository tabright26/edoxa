// Filename: StripeAccountResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Profiles
{
    internal sealed class StripeProfile : Profile
    {
        public StripeProfile()
        {
            //this.CreateMap<Account, StripeAccountDto>()
            //    .ForMember(account => account.Enabled, config => config.MapFrom(account => !account.Requirements.CurrentlyDue.Any()));

            //this.CreateMap<BankAccount, StripeBankAccountDto>()
            //    .ForMember(bankAccount => bankAccount.BankName, config => config.MapFrom(bankAccount => bankAccount.BankName))
            //    .ForMember(bankAccount => bankAccount.Country, config => config.MapFrom(bankAccount => bankAccount.Country))
            //    .ForMember(bankAccount => bankAccount.Currency, config => config.MapFrom(bankAccount => bankAccount.Currency))
            //    .ForMember(bankAccount => bankAccount.Last4, config => config.MapFrom(bankAccount => bankAccount.Last4))
            //    .ForMember(bankAccount => bankAccount.Status, config => config.MapFrom(bankAccount => bankAccount.Status))
            //    .ForMember(bankAccount => bankAccount.DefaultForCurrency, config => config.MapFrom(bankAccount => bankAccount.DefaultForCurrency));

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
