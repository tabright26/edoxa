// Filename: TestDataFixture.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Factories;
using eDoxa.Challenges.Api.Infrastructure.Data.Storage;

namespace eDoxa.Challenges.TestHelper.Fixtures
{
    public sealed class TestDataFixture
    {
        public TestDataFixture()
        {
            FileStorage = new FileStorage();
            FakerFactory = new FakerFactory();
        }

        public FakerFactory FakerFactory { get; }

        public FileStorage FileStorage { get; }
    }
}
