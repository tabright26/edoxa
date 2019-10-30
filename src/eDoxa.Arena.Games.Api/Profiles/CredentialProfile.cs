// Filename: CredentialProfile.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Seedwork.Application.Responses;

namespace eDoxa.Arena.Games.Api.Profiles
{
    internal sealed class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            this.CreateMap<Credential, CredentialResponse>()
                .ForMember(gameCredential => gameCredential.UserId, config => config.MapFrom(gameCredential => gameCredential.UserId))
                .ForMember(gameCredential => gameCredential.Game, config => config.MapFrom(gameCredential => gameCredential.Game))
                .ForMember(gameCredential => gameCredential.PlayerId, config => config.MapFrom(gameCredential => gameCredential.PlayerId));
        }
    }
}
