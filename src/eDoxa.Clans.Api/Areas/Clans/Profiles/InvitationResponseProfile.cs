// Filename: InvitationResponseProfile.cs
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
    internal sealed class InvitationResponseProfile : Profile
    {
        public InvitationResponseProfile()
        {
            this.CreateMap<Invitation, InvitationResponse>()
                .ForMember(invitation => invitation.Id, config => config.MapFrom<Guid>(invitation => invitation.Id))
                .ForMember(invitation => invitation.UserId, config => config.MapFrom<Guid>(invitation => invitation.UserId))
                .ForMember(invitation => invitation.ClanId, config => config.MapFrom<Guid>(invitation => invitation.ClanId));
        }
    }
}
