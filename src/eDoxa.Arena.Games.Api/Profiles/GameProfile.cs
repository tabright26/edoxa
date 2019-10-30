// Filename: GameProfile.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Games.Api.Responses;
using eDoxa.Arena.Games.Domain.AggregateModels;

namespace eDoxa.Arena.Games.Api.Profiles
{
    internal sealed class GameProfile : Profile
    {
        public GameProfile()
        {
            this.CreateMap<GameInfo, GameResponse>()
                .ForMember(gameInfo => gameInfo.Name, config => config.MapFrom(gameInfo => gameInfo.Name))
                .ForMember(gameInfo => gameInfo.DisplayName, config => config.MapFrom(gameInfo => gameInfo.DisplayName))
                .ForMember(gameInfo => gameInfo.ImageName, config => config.MapFrom(gameInfo => gameInfo.ImageName))
                .ForMember(gameInfo => gameInfo.ReactComponent, config => config.MapFrom(gameInfo => gameInfo.ReactComponent))
                .ForMember(gameInfo => gameInfo.Linked, config => config.MapFrom(gameInfo => gameInfo.Linked))
                .ForMember(
                    gameInfo => gameInfo.Services,
                    config => config.MapFrom(
                        gameInfo => gameInfo.Services.ToDictionary(
                            service => service.Key.ToLowerInvariant(),
                            service => new ServiceResponse
                            {
                                Displayed = service.Value.Displayed,
                                Enabled = service.Value.Enabled
                            })));
        }
    }
}
