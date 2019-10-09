// Filename: PaymentApiModule.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Payment.Api.Areas.Stripe.Services;
using eDoxa.Payment.Domain.Repositories;
using eDoxa.Payment.Domain.Services;
using eDoxa.Payment.Infrastructure.Repositories;

namespace eDoxa.Payment.Api.Infrastructure
{
    internal sealed class PaymentApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterType<StripeRepository>().As<IStripeRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<StripeService>().As<IStripeService>().InstancePerLifetimeScope();
            builder.RegisterType<StripeCustomerService>().As<IStripeCustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<StripeAccountService>().As<IStripeAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<StripeExternalAccountService>().As<IStripeExternalAccountService>().InstancePerLifetimeScope();
        }
    }
}
