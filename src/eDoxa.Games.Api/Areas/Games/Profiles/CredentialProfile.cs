// Filename: CredentialProfile.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Games.Api.Areas.Games.Responses;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;

namespace eDoxa.Games.Api.Areas.Games.Profiles
{
    internal sealed class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            this.CreateMap<Credential, CredentialResponse>()
                .ForMember(credential => credential.UserId, config => config.MapFrom(credential => credential.UserId))
                .ForMember(credential => credential.Game, config => config.MapFrom(credential => credential.Game))
                .ForMember(credential => credential.PlayerId, config => config.MapFrom(credential => credential.PlayerId));
        }
    }
}
