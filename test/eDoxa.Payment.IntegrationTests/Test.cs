// Filename: Test.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Payment.IntegrationTests
{
    public sealed class Test : IntegrationTest
    {
        public Test(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        [Fact]
        public void TestMethod()
        {
            Assert.True(true);
        }
    }
}
