// Filename: Test.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Notifications.IntegrationTests
{
    public sealed class Test : IntegrationTest
    {
        public Test(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        [Fact]
        public void TestMethod()
        {
            Assert.True(true);
        }
    }
}
