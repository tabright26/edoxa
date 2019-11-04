﻿using System;

using AutoMapper;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Responses.Profiles
{
    public class DivisionResponseProfile : Profile
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
