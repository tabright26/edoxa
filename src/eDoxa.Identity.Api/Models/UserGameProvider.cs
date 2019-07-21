// Filename: UserGameProvider.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Identity.Api.Models
{
    public class UserGameProvider
    {
        public int Game { get; set; }

        public string PlayerId { get; set; }

        public Guid UserId { get; set; }
    }
}
