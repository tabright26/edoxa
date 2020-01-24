// Filename: PaymentDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextCleaner : DbContextCleaner
    {
        public PaymentDbContextCleaner(PaymentDbContext context, IWebHostEnvironment environment, ILogger<PaymentDbContextCleaner> logger) : base(
            context,
            environment,
            logger)
        {
        }
    }
}
