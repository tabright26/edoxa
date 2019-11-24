// Filename: FakeAuthenticationOptions.cs
// Date Created: 2019-08-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;

#nullable disable

namespace eDoxa.Seedwork.TestHelper
{
    public class FakeAuthenticationOptions : AuthenticationSchemeOptions
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
