// Filename: UnitTest.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Payment.UnitTests.TestHelpers
{
    public abstract class UnitTestClass : IClassFixture<TestDataFixture>
    {
        protected UnitTestClass(TestDataFixture testData)
        {
            TestData = testData;
        }

        protected TestDataFixture TestData { get; }
    }
}
