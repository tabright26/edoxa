// Filename: CredentialProfile.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Games.Api.Areas.Credentials.Responses;
using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;

namespace eDoxa.Arena.Games.Api.Areas.Credentials.Profiles
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
