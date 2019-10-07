// Filename: PaymentDbContextSeeder.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextSeeder : DbContextSeeder
    {
        private readonly PaymentDbContext _context;

        public PaymentDbContextSeeder(PaymentDbContext context, IHostingEnvironment environment, ILogger<PaymentDbContextSeeder> logger) : base(
            environment,
            logger)
        {
            _context = context;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!await _context.StripeReferences.AnyAsync())
            {
                var stripeReference = new StripeReference(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"), "cus_F5L8mRzm6YN5ma", "acct_1EbASfAPhMnJQouG");

                _context.StripeReferences.Add(stripeReference);

                await _context.SaveChangesAsync();

                Logger.LogInformation("The stripe references being populated.");
            }
            else
            {
                Logger.LogInformation("The stripe references already populated.");
            }
        }
    }
}
