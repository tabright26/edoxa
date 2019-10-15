// Filename: UserEmailProfile.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Areas.Identity.Responses.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, EmailResponse>()
                .ForMember(response => response.Email, config => config.MapFrom(user => user.Email))
                .ForMember(response => response.EmailVerified, config => config.MapFrom(user => user.EmailConfirmed));

            this.CreateMap<User, PhoneResponse>()
                .ForMember(response => response.PhoneNumber, config => config.MapFrom(user => user.PhoneNumber))
                .ForMember(response => response.PhoneNumberVerified, config => config.MapFrom(user => user.PhoneNumberConfirmed));
        }
    }
}
