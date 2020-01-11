// Filename: UserProfile.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Identity.Api.Application.Profiles
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<Domain.AggregateModels.UserAggregate.UserProfile, ProfileDto>()
                .ForMember(profile => profile.Name, config => config.MapFrom(profile => profile.ToString()))
                .ForMember(profile => profile.FirstName, config => config.MapFrom(profile => profile.FirstName))
                .ForMember(profile => profile.LastName, config => config.MapFrom(profile => profile.LastName))
                .ForMember(profile => profile.Gender, config => config.MapFrom(profile => profile.Gender.ToEnum<EnumGender>()))
                .ForMember(profile => profile.Dob, config => config.MapFrom(profile => profile.Dob));

            this.CreateMap<UserDob, DobDto>()
                .ForMember(dob => dob.Year, config => config.MapFrom(dob => dob.Year))
                .ForMember(dob => dob.Month, config => config.MapFrom(dob => dob.Month))
                .ForMember(dob => dob.Day, config => config.MapFrom(dob => dob.Day));
                
            this.CreateMap<User, EmailDto>()
                .ForMember(email => email.Address, config => config.MapFrom(user => user.Email))
                .ForMember(email => email.Verified, config => config.MapFrom(user => user.EmailConfirmed));

            this.CreateMap<User, PhoneDto>()
                .ForMember(phone => phone.Number, config => config.MapFrom(user => user.PhoneNumber))
                .ForMember(phone => phone.Verified, config => config.MapFrom(user => user.PhoneNumberConfirmed));
        }
    }
}
