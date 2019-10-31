// Filename: IntegrationTestClass.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Organizations.Clans.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Organizations.Clans.TestHelper
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
