using System;

using AutoMapper;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Responses.Profiles
{
    public class ClanResponseProfile : Profile
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
