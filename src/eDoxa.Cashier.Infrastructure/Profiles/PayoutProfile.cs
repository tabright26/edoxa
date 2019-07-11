// Filename: PayoutProfile.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class PayoutProfile : Profile
    {
        public PayoutProfile()
        {
            this.CreateMap<ICollection<BucketModel>, IPayout>().ConvertUsing(new PayoutTypeConverter());
        }
    }
}
