﻿// Filename: Test.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.TestHelpers;
using eDoxa.Notifications.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Notifications.IntegrationTests
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