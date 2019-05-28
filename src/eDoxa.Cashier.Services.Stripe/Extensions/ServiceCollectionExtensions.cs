// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Services.Stripe.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using Stripe;

namespace eDoxa.Cashier.Services.Stripe.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStripe(this IServiceCollection services)
        {
            services.AddTransient<AccountService>();

            services.AddTransient<CardService>();

            services.AddTransient<CustomerService>();

            services.AddTransient<ExternalAccountService>();

            services.AddTransient<InvoiceService>();

            services.AddTransient<InvoiceItemService>();

            services.AddTransient<TransferService>();

            services.AddTransient<IStripeService, StripeService>();
        }
    }
}
