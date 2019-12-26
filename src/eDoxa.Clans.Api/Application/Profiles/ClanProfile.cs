// Filename: ClanProfile.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Grpc.Protos.Clans.Dtos;

namespace eDoxa.Clans.Api.Application.Profiles
{
    internal sealed class ClanProfile : Profile
    {
        public ClanProfile()
        {
            this.CreateMap<Clan, ClanDto>()
                .ForMember(clan => clan.Id, config => config.MapFrom(clan => clan.Id.ToString()))
                .ForMember(clan => clan.Name, config => config.MapFrom(clan => clan.Name))
                .ForMember(clan => clan.Summary, config => config.MapFrom(clan => clan.Summary))
                .ForMember(clan => clan.OwnerId, config => config.MapFrom(clan => clan.OwnerId.ToString()))
                .ForMember(clan => clan.Members, config => config.MapFrom(clan => clan.Members))
                .ForMember(clan => clan.Divisions, config => config.MapFrom(clan => clan.Divisions));

            this.CreateMap<Member, MemberDto>()
                .ForMember(member => member.Id, config => config.MapFrom(member => member.Id.ToString()))
                .ForMember(member => member.UserId, config => config.MapFrom(member => member.UserId.ToString()))
                .ForMember(member => member.ClanId, config => config.MapFrom(member => member.ClanId.ToString()));

            this.CreateMap<Division, DivisionDto>()
                .ForMember(division => division.Id, config => config.MapFrom(member => member.Id.ToString()))
                .ForMember(division => division.ClanId, config => config.MapFrom(member => member.ClanId.ToString()))
                .ForMember(division => division.Name, config => config.MapFrom(clan => clan.Name))
                .ForMember(division => division.Description, config => config.MapFrom(clan => clan.Description))
                .ForMember(division => division.Members, config => config.MapFrom(clan => clan.Members));
        }
    }
}
