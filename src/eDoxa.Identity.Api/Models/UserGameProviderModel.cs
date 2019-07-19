// Filename: GameModel.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Identity.Api.Models
{
    public class UserGameProviderModel
    {
        public int GameProvider { get; set; }

        public string ProviderKey { get; set; }

        public Guid UserId { get; set; }
    }
}
