// Filename: UserEmailProfile.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Responses;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, EmailResponse>()
                .ForMember(response => response.Address, config => config.MapFrom(user => user.Email))
                .ForMember(response => response.Verified, config => config.MapFrom(user => user.EmailConfirmed));

            this.CreateMap<User, PhoneResponse>()
                .ForMember(response => response.Number, config => config.MapFrom(user => user.PhoneNumber))
                .ForMember(response => response.Verified, config => config.MapFrom(user => user.PhoneNumberConfirmed));
        }
    }
}
