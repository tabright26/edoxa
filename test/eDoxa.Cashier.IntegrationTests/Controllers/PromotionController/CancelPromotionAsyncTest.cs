// Filename: CancelPromotionAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using Microsoft.AspNetCore.Routing;

namespace eDoxa.Cashier.IntegrationTests.Controllers.PromotionController
{
    public sealed class CancelPromotionAsyncTest : IntegrationTest // GABRIEL: INTEGRATION TESTS
    {
        public CancelPromotionAsyncTest(
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
