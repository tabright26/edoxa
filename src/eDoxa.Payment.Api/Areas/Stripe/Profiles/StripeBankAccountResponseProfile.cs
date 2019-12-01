// Filename: StripeBankAccountResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Payment.Responses;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripeBankAccountResponseProfile : Profile
    {
        public StripeBankAccountResponseProfile()
        {
            this.CreateMap<BankAccount, StripeBankAccountResponse>()
                .ForMember(bankAccount => bankAccount.BankName, config => config.MapFrom(bankAccount => bankAccount.BankName))
                .ForMember(bankAccount => bankAccount.Country, config => config.MapFrom(bankAccount => bankAccount.Country))
                .ForMember(bankAccount => bankAccount.Currency, config => config.MapFrom(bankAccount => bankAccount.Currency))
                .ForMember(bankAccount => bankAccount.Last4, config => config.MapFrom(bankAccount => bankAccount.Last4))
                .ForMember(bankAccount => bankAccount.Status, config => config.MapFrom(bankAccount => bankAccount.Status))
                .ForMember(bankAccount => bankAccount.DefaultForCurrency, config => config.MapFrom(bankAccount => bankAccount.DefaultForCurrency));
        }
    }
}
