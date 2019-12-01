// Filename: StripeAccountResponseProfile.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using AutoMapper;

using eDoxa.Payment.Responses;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Profiles
{
    internal sealed class StripeAccountResponseProfile : Profile
    {
        public StripeAccountResponseProfile()
        {
            this.CreateMap<Account, StripeAccountResponse>()
                .ForMember(account => account.Enabled, config => config.MapFrom(account => !account.Requirements.CurrentlyDue.Any()));
        }
    }
}
