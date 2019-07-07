// Filename: TransactionStatusSuccededPaymentStartup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Providers.Stripe.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.FunctionalTests.Services.Payment.Helpers.Startups
{
    internal sealed class TransactionStatusSuccededTestPaymentStartup : TestPaymentStartup
    {
        public TransactionStatusSuccededTestPaymentStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }

        protected override IServiceProvider BuildModule(IServiceCollection services)
        {
            services.AddTransient<IStripeService, MockStripeService>();

            return base.BuildModule(services);
        }

        private sealed class MockStripeService : IStripeService
        {
            public Task<string> CreateAccountAsync(
                Guid userId,
                string email,
                string firstName,
                string lastName,
                int year,
                int month,
                int day,
                CancellationToken cancellationToken = default
            )
            {
                return Task.FromResult("acct_test");
            }

            public Task<string> CreateCustomerAsync(
                Guid userId,
                string connectAccountId,
                string email,
                CancellationToken cancellationToken = default
            )
            {
                return Task.FromResult("cus_test");
            }

            public Task CreateInvoiceAsync(
                Guid transactionId,
                string transactionDescription,
                string customerId,
                long amount,
                CancellationToken cancellationToken = default
            )
            {
                return Task.CompletedTask;
            }

            public Task CreateTransferAsync(
                Guid transactionId,
                string transactionDescription,
                string connectAccountId,
                long amount,
                CancellationToken cancellationToken = default
            )
            {
                return Task.CompletedTask;
            }
        }
    }
}
