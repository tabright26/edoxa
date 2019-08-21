// Filename: UserDoxaTag.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

#nullable disable

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class UserDoxaTag
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"{Name}#{Code}";
        }
    }
}
