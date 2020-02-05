// Filename: PaymentApiModule.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Payment.Api.Application.Stripe.Services;
using eDoxa.Payment.Api.Infrastructure.Data;
using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.Infrastructure.Repositories;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

namespace eDoxa.Payment.Api.Infrastructure
{
    internal sealed class PaymentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Seeder
            builder.RegisterType<PaymentDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<StripeRepository>().As<IStripeRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<StripeService>().As<IStripeService>().InstancePerLifetimeScope();
            builder.RegisterType<StripeCustomerService>().As<IStripeCustomerService>().InstancePerDependency();
            //builder.RegisterType<StripeAccountService>().As<IStripeAccountService>().InstancePerDependency();
            //builder.RegisterType<StripeExternalAccountService>().As<IStripeExternalAccountService>().InstancePerDependency();
            builder.RegisterType<StripePaymentMethodService>().As<IStripePaymentMethodService>().InstancePerDependency();
            builder.RegisterType<StripeInvoiceService>().As<IStripeInvoiceService>().InstancePerDependency();
            builder.RegisterType<StripeInvoiceItemService>().As<IStripeInvoiceItemService>().InstancePerDependency();
            //builder.RegisterType<StripeTransferService>().As<IStripeTransferService>().InstancePerDependency();
        }
    }
}
