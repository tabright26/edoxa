// Filename: UserModel.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Seedwork.Security.Models
{
    public class UserModel : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
