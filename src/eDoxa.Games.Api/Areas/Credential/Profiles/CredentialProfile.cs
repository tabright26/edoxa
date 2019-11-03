// Filename: CredentialProfile.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Games.Api.Areas.Credential.Responses;

namespace eDoxa.Games.Api.Areas.Credential.Profiles
{
    internal sealed class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            this.CreateMap<Domain.AggregateModels.CredentialAggregate.Credential, CredentialResponse>()
                .ForMember(credential => credential.UserId, config => config.MapFrom(credential => credential.UserId))
                .ForMember(credential => credential.Game, config => config.MapFrom(credential => credential.Game))
                .ForMember(credential => credential.PlayerId, config => config.MapFrom(credential => credential.PlayerId));
        }
    }
}
