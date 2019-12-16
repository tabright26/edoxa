// Filename: CredentialProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Games.Api.Application.Profiles
{
    internal sealed class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            this.CreateMap<Credential, CredentialDto>()
                .ForMember(credential => credential.UserId, config => config.MapFrom(credential => credential.UserId.ToString()))
                .ForMember(credential => credential.GamePlayerId, config => config.MapFrom(credential => credential.PlayerId.ToString()))
                .ForMember(credential => credential.Game, config => config.MapFrom(credential => credential.Game.ToEnum<GameDto>()));
        }
    }
}
