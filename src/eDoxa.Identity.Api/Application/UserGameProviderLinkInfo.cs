// Filename: UserGameProviderLinkInfo.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Application
{
    public class UserGameProviderLinkInfo
    {
        public GameProvider GameProvider { get; set; }

        public bool IsLinked { get; set; }
    }
}
