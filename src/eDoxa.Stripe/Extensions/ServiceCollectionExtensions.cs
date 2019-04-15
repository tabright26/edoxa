// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-15
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.Extensions.DependencyInjection;

using Stripe;

namespace eDoxa.Stripe.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStripe(this IServiceCollection services)
        {
            services.AddTransient<CardService>();

            services.AddTransient<CustomerService>();

            services.AddTransient<InvoiceService>();

            services.AddTransient<InvoiceItemService>();
        }
    }
}