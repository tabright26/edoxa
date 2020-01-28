// Filename: IntegrationTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.TestHelper.Fixtures;

using Microsoft.AspNetCore.Routing;

using Xunit;

namespace eDoxa.Cashier.TestHelper
{
    public abstract class IntegrationTest : IClassFixture<TestHostFixture>, IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTest(
            TestHostFixture testHost,
            TestDataFixture testData,
            TestMapperFixture testMapper,
            Func<HttpClient, LinkGenerator, object, Task<HttpResponseMessage>>? executeAsync = null
        )
        {
            TestHost = testHost;
            TestData = testData;
            TestMapper = testMapper.Instance;
            ExecuteFuncAsync = executeAsync ?? ((httpClient, linkGenerator, values) => Task.FromResult(new HttpResponseMessage()));
        }

        protected TestHostFixture TestHost { get; }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }

        protected Func<HttpClient, LinkGenerator, object, Task<HttpResponseMessage>> ExecuteFuncAsync { get; }
    }
}
