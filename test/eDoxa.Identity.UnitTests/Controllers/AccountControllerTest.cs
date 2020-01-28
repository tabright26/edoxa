// Filename: AccountControllerTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class AccountControllerTest : UnitTest // GABRIEL: UNIT TESTS
    {
        public AccountControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }
    }
}
