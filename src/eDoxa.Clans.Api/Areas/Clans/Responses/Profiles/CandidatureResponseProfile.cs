using System;

using AutoMapper;

using eDoxa.Clans.Domain.Models;

namespace eDoxa.Clans.Api.Areas.Clans.Responses.Profiles
{
    public class CandidatureResponseProfile : Profile
    {
        public CandidatureResponseProfile()
        {
            this.CreateMap<Candidature, CandidatureResponse>()
                .ForMember(candidature => candidature.Id, config => config.MapFrom<Guid>(candidature => candidature.Id))
                .ForMember(candidature => candidature.UserId, config => config.MapFrom<Guid>(candidature => candidature.UserId))
                .ForMember(candidature => candidature.ClanId, config => config.MapFrom<Guid>(candidature => candidature.ClanId));
        }
    }
}
