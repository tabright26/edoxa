// Filename: PaymentApiModule.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Payment.Api.Application.Stripe.Services;
using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;

namespace eDoxa.Payment.Api.Infrastructure
{
    internal sealed class PaymentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<StripeCustomerService>().As<IStripeCustomerService>().InstancePerDependency();
            builder.RegisterType<StripePaymentMethodService>().As<IStripePaymentMethodService>().InstancePerDependency();
            builder.RegisterType<StripeInvoiceService>().As<IStripeInvoiceService>().InstancePerDependency();
            builder.RegisterType<StripeInvoiceItemService>().As<IStripeInvoiceItemService>().InstancePerDependency();
        }
    }
}
