using System;

using AutoMapper;

using eDoxa.Clans.Domain.Models;

namespace eDoxa.Clans.Api.Areas.Clans.Responses.Profiles
{
    public class MemberResponseProfile : Profile
    {
        public MemberResponseProfile()
        {
            this.CreateMap<Member, MemberResponse>()
                .ForMember(member => member.Id, config => config.MapFrom<Guid>(member => member.Id))
                .ForMember(member => member.UserId, config => config.MapFrom<Guid>(member => member.UserId))
                .ForMember(member => member.ClanId, config => config.MapFrom<Guid>(member => member.ClanId));
        }
    }
}
