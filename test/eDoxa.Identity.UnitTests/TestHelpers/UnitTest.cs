// Filename: UnitTest.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Identity.UnitTests.TestHelpers
{
    public abstract class UnitTest : IClassFixture<IdentityFakerFixture>
    {
        protected UnitTest(IdentityFakerFixture faker)
        {
            Faker = faker;
        }

        protected IdentityFakerFixture Faker { get; }
    }
}
