// Filename: MockHttpContextAccessorExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Security.Claims;

using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Extensions
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
                        new Claim(ChallengeGame.LeagueOfLegends.GetClaimType(), "123124124124")
                    }
                );
        }
    }
}
