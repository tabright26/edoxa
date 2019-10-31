// Filename: IntegrationTest.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Notifications.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Notifications.TestHelper
{
    public abstract class IntegrationTest : IClassFixture<TestApiFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTest(TestApiFixture testApi, TestMapperFixture testMapper)
        {
            TestApi = testApi;
            TestMapper = testMapper.Instance;
        }

        protected TestApiFixture TestApi { get; }

        protected IMapper TestMapper { get; }
    }
}
