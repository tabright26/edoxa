// Filename: PaymentMethodAttachControllerPostAsyncTest.cs
// Date Created: 2019-10-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

namespace eDoxa.Payment.IntegrationTests.Areas.Stripe.Controllers
{
    public sealed class PaymentMethodAttachControllerPostAsyncTest : IntegrationTest // GABRIEL: INTEGRATION TEST
    {
        public PaymentMethodAttachControllerPostAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }
    }
}
