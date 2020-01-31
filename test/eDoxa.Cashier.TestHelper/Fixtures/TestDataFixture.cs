// Filename: TestDataFixture.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers.Factories;
using eDoxa.Cashier.Api.Infrastructure.Data.Storage;

namespace eDoxa.Cashier.TestHelper.Fixtures
{
    public sealed class TestDataFixture
    {
        public TestDataFixture()
        {
            FakerFactory = new FakerFactory();
            FileStorage = new FileStorage();
        }

        public FakerFactory FakerFactory { get; }

        public FileStorage FileStorage { get; }
    }
}
