// Filename: ClanResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Responses;

namespace eDoxa.Clans.Api.Profiles
{
    internal sealed class ClanResponseProfile : Profile
    {
        public ClanResponseProfile()
        {
            this.CreateMap<Clan, ClanResponse>()
                .ForMember(clan => clan.Id, config => config.MapFrom<Guid>(clan => clan.Id))
                .ForMember(clan => clan.Name, config => config.MapFrom(clan => clan.Name))
                .ForMember(clan => clan.Summary, config => config.MapFrom(clan => clan.Summary))
                .ForMember(clan => clan.OwnerId, config => config.MapFrom<Guid>(clan => clan.OwnerId))
                .ForMember(clan => clan.Members, config => config.MapFrom(clan => clan.Members))
                .ForMember(clan => clan.Divisions, config => config.MapFrom(clan => clan.Divisions));
        }
    }
}
