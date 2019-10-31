// Filename: GameProfile.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Games.Api.Areas.Games.Responses;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Api.Areas.Games.Profiles
{
    internal sealed class GameProfile : Profile
    {
        public GameProfile()
        {
            this.CreateMap<Game, GameResponse>().ForMember(game => game.NormalizedName, config => config.MapFrom(game => game.NormalizedName));
        }
    }
}
