// Filename: StripeBankAccountResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripeBankAccountResponseProfile : Profile
    {
        public StripeBankAccountResponseProfile()
        {
            this.CreateMap<BankAccount, StripeBankAccountDto>()
                .ForMember(bankAccount => bankAccount.BankName, config => config.MapFrom(bankAccount => bankAccount.BankName))
                .ForMember(bankAccount => bankAccount.Country, config => config.MapFrom(bankAccount => bankAccount.Country))
                .ForMember(bankAccount => bankAccount.Currency, config => config.MapFrom(bankAccount => bankAccount.Currency))
                .ForMember(bankAccount => bankAccount.Last4, config => config.MapFrom(bankAccount => bankAccount.Last4))
                .ForMember(bankAccount => bankAccount.Status, config => config.MapFrom(bankAccount => bankAccount.Status))
                .ForMember(bankAccount => bankAccount.DefaultForCurrency, config => config.MapFrom(bankAccount => bankAccount.DefaultForCurrency));
        }
    }
}
