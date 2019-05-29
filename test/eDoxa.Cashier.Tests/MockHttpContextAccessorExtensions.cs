﻿// Filename: MockHttpContextAccessorExtensions.cs
// Date Created: 2019-05-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Security.Claims;

using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Arena.Challenges.Tests
{
    public static class MockHttpContextAccessorExtensions
    {
        public static void SetupClaims(this Mock<IHttpContextAccessor> mockHttpContextAccessor)
        {
            mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims)
                .Returns(
                    new HashSet<Claim>
                    {
                        new Claim(JwtClaimTypes.Subject, "5C43502B-FCE8-4235-8557-C22D2A638AD7"),
                        new Claim(Game.LeagueOfLegends.GetClaimType(), "123124124124")
                    }
                );
        }
    }
}
