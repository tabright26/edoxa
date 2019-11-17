// Filename: PaymentDbContextSeeder.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextSeeder : DbContextSeeder
    {
        private readonly IStripeReferenceService _stripeReferenceService;

        public PaymentDbContextSeeder(IStripeReferenceService stripeReferenceService, IHostingEnvironment environment, ILogger<PaymentDbContextSeeder> logger) : base(
            environment,
            logger)
        {
            _stripeReferenceService = stripeReferenceService;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            var adminId = UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091");

            if (!await _stripeReferenceService.ReferenceExistsAsync(adminId))
            {
                await _stripeReferenceService.CreateReferenceAsync(adminId, "cus_F5L8mRzm6YN5ma", "acct_1EbASfAPhMnJQouG");

                Logger.LogInformation("The stripe references being populated.");
            }
            else
            {
                Logger.LogInformation("The stripe references already populated.");
            }
        }
    }
}
