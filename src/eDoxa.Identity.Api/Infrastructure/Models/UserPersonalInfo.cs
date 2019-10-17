// Filename: Informations.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    public class UserInformations
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
