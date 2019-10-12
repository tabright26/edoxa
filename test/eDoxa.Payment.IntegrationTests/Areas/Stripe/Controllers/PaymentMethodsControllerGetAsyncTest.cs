﻿// Filename: PaymentMethodsControllerGetAsyncTest.cs
// Date Created: 2019-10-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

namespace eDoxa.Payment.IntegrationTests.Areas.Stripe.Controllers
{
    public sealed class PaymentMethodsControllerGetAsyncTest : IntegrationTest // GABRIEL: INTEGRATION TEST
    {
        public PaymentMethodsControllerGetAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }
    }
}
