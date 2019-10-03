// Filename: UnitTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Payment.TestHelpers
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>
    {
        protected UnitTest(TestDataFixture testData)
        {
            TestData = testData;
        }

        protected TestDataFixture TestData { get; }
    }
}
