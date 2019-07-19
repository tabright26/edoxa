// Filename: UserGameProviderInfo.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Application
{
    public sealed class UserGameProviderInfo
    {
        public UserGameProviderInfo(GameProvider gameProvider, string providerKey)
        {
            GameProvider = gameProvider;
            ProviderKey = providerKey;
        }

        public GameProvider GameProvider { get; }

        public string ProviderKey { get; }
    }
}
