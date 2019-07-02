// Filename: UserProfile.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.ConverterTypes;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<UserModel, User>().ConvertUsing(new UserTypeConverter());

            this.CreateMap<User, UserModel>()
                .ForMember(user => user.Id, config => config.MapFrom<Guid>(user => user.Id))
                .ForMember(user => user.ConnectAccountId, config => config.MapFrom(user => user.ConnectAccountId))
                .ForMember(user => user.CustomerId, config => config.MapFrom(user => user.CustomerId))
                .ForMember(user => user.BankAccountId, config => config.MapFrom(user => user.BankAccountId))
                .ForMember(user => user.Account, config => config.Ignore());
        }
    }
}
