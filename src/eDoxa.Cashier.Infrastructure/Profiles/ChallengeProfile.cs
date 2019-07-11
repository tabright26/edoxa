// Filename: ChallengeProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<ChallengeModel, IChallenge>().ConvertUsing(new ChallengeConverter());
        }
    }
}
