// Filename: FakeAuthenticationOptions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;

namespace eDoxa.Seedwork.TestHelper
{
    public sealed class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string AuthenticationScheme { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
