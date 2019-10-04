// Filename: PaymentDbContextSeeder.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextSeeder : DbContextSeeder
    {
        private readonly PaymentDbContext _context;

        public PaymentDbContextSeeder(PaymentDbContext context, IHostingEnvironment environment, ILogger<PaymentDbContextSeeder> logger) : base(environment, logger)
        {
            _context = context;
        }
    }
}
