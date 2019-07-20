// Filename: UserGameProviderInfo.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Application
{
    public sealed class UserGameProviderInfo
    {
        public UserGameProviderInfo(Game game, string playerId)
        {
            Game = game;
            PlayerId = playerId;
        }

        public Game Game { get; }

        public string PlayerId { get; }
    }
}
