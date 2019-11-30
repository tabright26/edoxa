// Filename: DoxatagProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Responses;

namespace eDoxa.Identity.Api.Profiles
{
    internal sealed class DoxatagProfile : Profile
    {
        public DoxatagProfile()
        {
            this.CreateMap<Doxatag, DoxatagResponse>()
                .ForMember(doxatag => doxatag.UserId, config => config.MapFrom(doxatag => doxatag.UserId))
                .ForMember(doxatag => doxatag.Name, config => config.MapFrom(doxatag => doxatag.Name))
                .ForMember(doxatag => doxatag.Code, config => config.MapFrom(doxatag => doxatag.Code))
                .ForMember(doxatag => doxatag.Timestamp, config => config.MapFrom(doxatag => doxatag.Timestamp));
        }
    }
}
