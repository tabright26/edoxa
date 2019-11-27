// Filename: InformationsProfile.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Responses;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    public sealed class UserInformationsProfile : Profile
    {
        public UserInformationsProfile()
        {
            this.CreateMap<UserInformations, UserInformationsResponse>()
                .ForMember(
                    informations => informations.Name,
                    config =>
                    {
                        config.Condition(informations => informations.FirstName != null && informations.LastName != null);
                        config.MapFrom(informations => $"{informations.FirstName} {informations.LastName}");
                    }
                )
                .ForMember(informations => informations.FirstName, config => config.MapFrom(informations => informations.FirstName))
                .ForMember(informations => informations.LastName, config => config.MapFrom(informations => informations.LastName))
                .ForMember(informations => informations.Gender, config => config.MapFrom(informations => informations.Gender))
                .ForMember(informations => informations.Dob, config => config.MapFrom(informations => new DobResponse(informations.Dob.Year, informations.Dob.Month, informations.Dob.Day)));
        }
    }
}
