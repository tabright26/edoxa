﻿// Filename: IntegrationTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Challenges.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Arena.Challenges.TestHelpers
{
    public abstract class IntegrationTest : IClassFixture<TestApiFixture>, IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestApi = testApi;
            TestData = testData;
            TestMapper = testMapper.Instance;
        }

        protected TestApiFixture TestApi { get; }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}