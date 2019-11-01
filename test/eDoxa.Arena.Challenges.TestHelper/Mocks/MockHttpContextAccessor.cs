// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;

using eDoxa.Seedwork.Domain.Miscs;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Arena.Challenges.TestHelper.Mocks
{
    public sealed class MockHttpContextAccessor : Mock<IHttpContextAccessor>
    {
        public MockHttpContextAccessor()
        {
            this.Setup(accessor => accessor.HttpContext.User.Claims)
                .Returns(
                    new[]
                    {
                        new Claim(JwtClaimTypes.Subject, "5C43502B-FCE8-4235-8557-C22D2A638AD7"),
                        new Claim($"games/{Game.LeagueOfLegends.NormalizedName}", PlayerId.Parse("qwe213rq2131eqw"))
                    });
        }
    }
}
