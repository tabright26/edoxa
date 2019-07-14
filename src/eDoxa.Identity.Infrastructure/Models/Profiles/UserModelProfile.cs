// Filename: UserModelProfile.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Infrastructure.Models.Profiles
{
    internal sealed class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            this.CreateMap<User, UserModel>()
                .ForMember(user => user.Id, config => config.MapFrom<Guid>(user => user.Id))
                .ForMember(user => user.Email, config => config.MapFrom(user => user.Email.Address))
                .ForMember(user => user.NormalizedEmail, config => config.MapFrom(user => user.Email.Address.ToUpperInvariant()))
                .ForMember(user => user.EmailConfirmed, config => config.MapFrom(user => user.Email.Confirmed))
                .ForMember(user => user.UserName, config => config.MapFrom(user => user.Email.Address))
                .ForMember(user => user.NormalizedUserName, config => config.MapFrom(user => user.Email.Address.ToUpperInvariant()))
                .ForMember(user => user.PasswordHash, config => config.MapFrom(user => user.Password.Hash))
                .ForMember(user => user.FirstName, config => config.MapFrom(user => user.PersonalName.FirstName))
                .ForMember(user => user.LastName, config => config.MapFrom(user => user.PersonalName.LastName))
                .ForMember(user => user.PhoneNumber, config => config.MapFrom(user => user.Phone.Number))
                .ForMember(user => user.PhoneNumberConfirmed, config => config.MapFrom(user => user.Phone.Confirmed))
                .ForMember(user => user.BirthDate, config => config.MapFrom<DateTime>(user => user.BirthDate));
        }
    }
}
