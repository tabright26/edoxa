// Filename: ArenaServiceResponseProfile.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Games.Domain.AggregateModels;

namespace eDoxa.Arena.Games.Api.Areas.Games.Responses.Profiles
{
    internal sealed class ServiceResponseProfile : Profile
    {
        public ServiceResponseProfile()
        {
            this.CreateMap<ServiceInfo, ServiceResponse>()
                .ForMember(challengeInfo => challengeInfo.Displayed, config => config.MapFrom(challengeInfo => challengeInfo.Displayed))
                .ForMember(challengeInfo => challengeInfo.Enabled, config => config.MapFrom(challengeInfo => challengeInfo.Enabled));
        }
    }
}
