// Filename: TestModeProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Application.Profiles
{
    internal sealed class TestModeProfile : Profile
    {
        public TestModeProfile()
        {
            this.CreateMap<TestMode, TestModeViewModel>()
                .ForMember(testMode => testMode.State, config => config.MapFrom(testMode => testMode.StartingState))
                .ForMember(testMode => testMode.MatchQuantity, config => config.MapFrom(testMode => testMode.MatchQuantity))
                .ForMember(testMode => testMode.ParticipantQuantity, config => config.MapFrom(testMode => testMode.ParticipantQuantity))
                .ReverseMap()
                .ForMember(testMode => testMode.StartingState, config => config.MapFrom(testMode => testMode.State))
                .ForMember(testMode => testMode.MatchQuantity, config => config.MapFrom(testMode => testMode.MatchQuantity))
                .ForMember(testMode => testMode.ParticipantQuantity, config => config.MapFrom(testMode => testMode.ParticipantQuantity));
        }
    }
}
