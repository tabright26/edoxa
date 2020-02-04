// Filename: TestMockFixture.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.ServiceBus.Abstractions;

using Moq;

namespace eDoxa.Payment.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        public TestMockFixture()
        {
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
            StripeRepository = new Mock<IStripeRepository>();
            StripeAccountService = new Mock<IStripeAccountService>();
            StripeCustomerService = new Mock<IStripeCustomerService>();
            StripeExternalAccountService = new Mock<IStripeExternalAccountService>();
            StripeInvoiceItemService = new Mock<IStripeInvoiceItemService>();
            StripeInvoiceService = new Mock<IStripeInvoiceService>();
            StripePaymentMethodService = new Mock<IStripePaymentMethodService>();
            StripeService = new Mock<IStripeService>();
            StripeTransferService = new Mock<IStripeTransferService>();
        }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<IStripeRepository> StripeRepository { get; }
        
        public Mock<IStripeAccountService> StripeAccountService { get; }

        public Mock<IStripeCustomerService> StripeCustomerService { get; }

        public Mock<IStripeExternalAccountService> StripeExternalAccountService { get; }

        public Mock<IStripeInvoiceItemService> StripeInvoiceItemService { get; }

        public Mock<IStripeInvoiceService> StripeInvoiceService { get; }

        public Mock<IStripePaymentMethodService> StripePaymentMethodService { get; }

        public Mock<IStripeService> StripeService { get; }

        public Mock<IStripeTransferService> StripeTransferService { get; }
    }
}
