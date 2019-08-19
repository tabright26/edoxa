// Filename: UserTag.cs
// Date Created: 2019-08-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

#nullable disable

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class DoxaTag
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"{Name}#{Code}";
        }
    }
}
