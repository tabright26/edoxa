// Filename: UserGame.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class UserGame
    {
        public int Value { get; set; }

        public string PlayerId { get; set; }

        public Guid UserId { get; set; }
    }
}
