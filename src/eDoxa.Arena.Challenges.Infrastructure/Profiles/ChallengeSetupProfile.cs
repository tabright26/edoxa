// Filename: ChallengeSetupProfile.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles
{
    internal sealed class ChallengeSetupProfile : Profile
    {
        public ChallengeSetupProfile()
        {
            this.CreateMap<ChallengeSetupModel, ChallengeSetup>().ConvertUsing(new ChallengeSetupTypeConverter());
        }
    }
}
