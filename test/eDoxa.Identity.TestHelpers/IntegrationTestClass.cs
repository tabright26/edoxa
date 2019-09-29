// Filename: IntegrationTestClass.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using Xunit;

namespace eDoxa.Identity.TestHelpers
{
    public abstract class IntegrationTestClass : IClassFixture<TestApiFactory>, IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTestClass(TestApiFactory testApi, TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestApi = testApi;
            TestData = testData;
            TestMapper = testMapper.Instance;
        }

        protected TestApiFactory TestApi { get; }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}
