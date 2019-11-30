// Filename: MemberResponseProfile.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Responses;

namespace eDoxa.Clans.Api.Areas.Clans.Profiles
{
    internal sealed class MemberResponseProfile : Profile
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
