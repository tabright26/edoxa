// Filename: UserGameProviderInfo.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Application
{
    public sealed class UserGameProviderInfo
    {
        public UserGameProviderInfo(string name, string playerId)
        {
            Name = name;
            PlayerId = playerId;
        }

        public string Name { get; }

        public string PlayerId { get; }
    }
}
