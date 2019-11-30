// Filename: CandidatureResponseProfile.cs
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
    internal sealed class CandidatureResponseProfile : Profile
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
