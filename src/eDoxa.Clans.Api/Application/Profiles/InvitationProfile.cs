// Filename: InvitationProfile.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Grpc.Protos.Clans.Dtos;

namespace eDoxa.Clans.Api.Application.Profiles
{
    internal sealed class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            this.CreateMap<Invitation, InvitationDto>()
                .ForMember(invitation => invitation.Id, config => config.MapFrom(invitation => invitation.Id.ToString()))
                .ForMember(invitation => invitation.UserId, config => config.MapFrom(invitation => invitation.UserId.ToString()))
                .ForMember(invitation => invitation.ClanId, config => config.MapFrom(invitation => invitation.ClanId.ToString()));
        }
    }
}
