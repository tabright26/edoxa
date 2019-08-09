// Filename: Profile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class Profile
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
