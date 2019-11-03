using System;

using AutoMapper;

using eDoxa.Clans.Domain.Models;

namespace eDoxa.Clans.Api.Areas.Clans.Responses.Profiles
{
    public class InvitationResponseProfile : Profile
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
