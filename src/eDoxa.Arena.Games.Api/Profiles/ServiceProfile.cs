// Filename: ServiceProfile.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Games.Api.Responses;
using eDoxa.Arena.Games.Domain.AggregateModels;

namespace eDoxa.Arena.Games.Api.Profiles
{
    internal sealed class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            this.CreateMap<ServiceInfo, ServiceResponse>()
                .ForMember(challengeInfo => challengeInfo.Displayed, config => config.MapFrom(challengeInfo => challengeInfo.Displayed))
                .ForMember(challengeInfo => challengeInfo.Enabled, config => config.MapFrom(challengeInfo => challengeInfo.Enabled));
        }
    }
}
