// Filename: UserProfile.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Responses;

namespace eDoxa.Identity.Api.Application.Profiles
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<Domain.AggregateModels.UserAggregate.UserProfile, UserProfileResponse>()
                .ForMember(profile => profile.Name, config => config.MapFrom(profile => profile.ToString()))
                .ForMember(profile => profile.FirstName, config => config.MapFrom(profile => profile.FirstName))
                .ForMember(profile => profile.LastName, config => config.MapFrom(profile => profile.LastName))
                .ForMember(profile => profile.Gender, config => config.MapFrom(profile => profile.Gender.Name))
                .ForMember(profile => profile.Dob, config => config.MapFrom(profile => new DobResponse(profile.Dob.Year, profile.Dob.Month, profile.Dob.Day)));

            this.CreateMap<User, EmailResponse>()
                .ForMember(email => email.Address, config => config.MapFrom(user => user.Email))
                .ForMember(email => email.Verified, config => config.MapFrom(user => user.EmailConfirmed));

            this.CreateMap<User, PhoneResponse>()
                .ForMember(phone => phone.Number, config => config.MapFrom(user => user.PhoneNumber))
                .ForMember(phone => phone.Verified, config => config.MapFrom(user => user.PhoneNumberConfirmed));
        }
    }
}
