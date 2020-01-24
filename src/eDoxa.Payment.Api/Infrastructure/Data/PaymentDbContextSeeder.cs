// Filename: PaymentDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextSeeder : DbContextSeeder
    {
        public PaymentDbContextSeeder(PaymentDbContext context, IWebHostEnvironment environment, ILogger<PaymentDbContextSeeder> logger) : base(
            context,
            environment,
            logger)
        {
            StripeReferences = context.Set<StripeReference>();
        }

        private DbSet<StripeReference> StripeReferences { get; }

        protected override async Task SeedDevelopmentAsync()
        {
            var adminId = UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091");

            if (!await StripeReferences.AnyAsync(stripeReference => stripeReference.UserId == adminId))
            {
                StripeReferences.Add(new StripeReference(adminId, "cus_F5L8mRzm6YN5ma", "acct_1EbASfAPhMnJQouG"));

                await this.CommitAsync();

                Logger.LogInformation("The stripe references being populated.");
            }
            else
            {
                Logger.LogInformation("The stripe references already populated.");
            }
        }
    }
}
