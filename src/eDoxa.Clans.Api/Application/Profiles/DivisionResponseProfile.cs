// Filename: DivisionResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Responses;

namespace eDoxa.Clans.Api.Application.Profiles
{
    internal sealed class DivisionResponseProfile : Profile
    {
        public DivisionResponseProfile()
        {
            this.CreateMap<Division, DivisionResponse>()
                .ForMember(division => division.Id, config => config.MapFrom<Guid>(member => member.Id))
                .ForMember(division => division.ClanId, config => config.MapFrom<Guid>(member => member.ClanId))
                .ForMember(division => division.Name, config => config.MapFrom(clan => clan.Name))
                .ForMember(division => division.Description, config => config.MapFrom(clan => clan.Description))
                .ForMember(division => division.Members, config => config.MapFrom(clan => clan.Members));
        }
    }
}
