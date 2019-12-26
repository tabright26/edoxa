// Filename: CandidatureProfile.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Grpc.Protos.Clans.Dtos;

namespace eDoxa.Clans.Api.Application.Profiles
{
    internal sealed class CandidatureProfile : Profile
    {
        public CandidatureProfile()
        {
            this.CreateMap<Candidature, CandidatureDto>()
                .ForMember(candidature => candidature.Id, config => config.MapFrom(candidature => candidature.Id.ToString()))
                .ForMember(candidature => candidature.UserId, config => config.MapFrom(candidature => candidature.UserId.ToString()))
                .ForMember(candidature => candidature.ClanId, config => config.MapFrom(candidature => candidature.ClanId.ToString()));
        }
    }
}
