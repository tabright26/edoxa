// Filename: ChallengeSetupViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ChallengeSetupViewModelProfile : Profile
    {
        public ChallengeSetupViewModelProfile()
        {
            this.CreateMap<ChallengeSetup, ChallengeSetupViewModel>()
                .ForMember(setup => setup.BestOf, config => config.MapFrom<int>(setup => setup.BestOf))
                .ForMember(setup => setup.Entries, config => config.MapFrom<int>(setup => setup.Entries))
                .ForMember(setup => setup.PayoutEntries, config => config.MapFrom<int>(setup => setup.PayoutEntries))
                .ForMember(setup => setup.EntryFee, config => config.MapFrom(setup => setup.EntryFee));
        }
    }
}
