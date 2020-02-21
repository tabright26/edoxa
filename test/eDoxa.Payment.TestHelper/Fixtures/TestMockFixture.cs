// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.ServiceBus.Abstractions;
using eDoxa.Stripe.Services.Abstractions;

using Moq;

namespace eDoxa.Payment.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            StripeCustomerService = new Mock<IStripeCustomerService>();
            StripePaymentMethodService = new Mock<IStripePaymentMethodService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<IStripeCustomerService> StripeCustomerService { get; }

        public Mock<IStripePaymentMethodService> StripePaymentMethodService { get; }
    }
}
