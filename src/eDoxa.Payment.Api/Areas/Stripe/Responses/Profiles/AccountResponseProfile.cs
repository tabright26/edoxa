// Filename: AccountResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using AutoMapper;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses.Profiles
{
    internal sealed class AccountResponseProfile : Profile
    {
        public AccountResponseProfile()
        {
            this.CreateMap<Account, AccountResponse>()
                .ForMember(account => account.Enabled, config => config.MapFrom(account => !account.Requirements.CurrentlyDue.Any()));
        }
    }
}
