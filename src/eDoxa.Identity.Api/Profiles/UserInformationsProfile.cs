// Filename: InformationsProfile.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Responses;

namespace eDoxa.Identity.Api.Profiles
{
    public sealed class UserInformationsProfile : Profile
    {
        public UserInformationsProfile()
        {
            this.CreateMap<Domain.AggregateModels.UserAggregate.UserProfile, UserInformationsResponse>()
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
                .ForMember(informations => informations.Gender, config => config.MapFrom(informations => informations.Gender.Name))
                .ForMember(informations => informations.Dob, config => config.MapFrom(informations => new DobResponse(informations.Dob.Year, informations.Dob.Month, informations.Dob.Day)));
        }
    }
}
