// Filename: TestDataFixture.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Data.Storage;

namespace eDoxa.Identity.TestHelper.Fixtures
{
    public sealed class TestDataFixture
    {
        public TestDataFixture()
        {
            FileStorage = new FileStorage();
        }

        public FileStorage FileStorage { get; }
    }
}
