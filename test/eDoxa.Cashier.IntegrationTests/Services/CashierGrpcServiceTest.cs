// Filename: CashierGrpcServiceTest.cs
// Date Created: 2020-01-11
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using Microsoft.AspNetCore.Routing;

namespace eDoxa.Cashier.IntegrationTests.Services
{
    public sealed class CashierGrpcServiceTest : IntegrationTest // TODO: INTEGRATION TESTS
    {
        public CashierGrpcServiceTest(
            TestHostFixture testHost,
            TestDataFixture testData,
            TestMapperFixture testMapper,
            Func<HttpClient, LinkGenerator, object, Task<HttpResponseMessage>>? executeAsync = null
        ) : base(
            testHost,
            testData,
            testMapper,
            executeAsync)
        {
        }
    }
}
