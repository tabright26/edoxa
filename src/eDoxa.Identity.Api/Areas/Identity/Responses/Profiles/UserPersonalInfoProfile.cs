﻿// Filename: PersonalInfoProfile.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Areas.Identity.Responses.Profiles
{
    public class UserPersonalInfoProfile : Profile
    {
        public UserPersonalInfoProfile()
        {
            this.CreateMap<UserPersonalInfo, UserPersonalInfoResponse>()
                .ForMember(
                    personalInfo => personalInfo.Name,
                    config =>
                    {
                        config.Condition(personalInfo => personalInfo.FirstName != null && personalInfo.LastName != null);
                        config.MapFrom(personalInfo => $"{personalInfo.FirstName} {personalInfo.LastName}");
                    }
                )
                .ForMember(personalInfo => personalInfo.FirstName, config => config.MapFrom(personalInfo => personalInfo.FirstName))
                .ForMember(personalInfo => personalInfo.LastName, config => config.MapFrom(personalInfo => personalInfo.LastName))
                .ForMember(personalInfo => personalInfo.Gender, config => config.MapFrom(personalInfo => personalInfo.Gender))
                .ForMember(personalInfo => personalInfo.BirthDate, config => config.MapFrom(personalInfo => personalInfo.BirthDate));
        }
    }
}