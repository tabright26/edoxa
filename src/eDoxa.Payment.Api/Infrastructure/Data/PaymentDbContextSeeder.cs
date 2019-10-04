// Filename: PaymentDbContextSeeder.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextSeeder : IDbContextSeeder
    {
        private readonly PaymentDbContext _context;
        private readonly ILogger<PaymentDbContextSeeder> _logger;
        private readonly IHostingEnvironment _environment;

        public PaymentDbContextSeeder(ILogger<PaymentDbContextSeeder> logger, IHostingEnvironment environment, PaymentDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public Task SeedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
