﻿// Filename: PaymentDbContextCleaner.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Payment.Api.Infrastructure.Data
{
    internal sealed class PaymentDbContextCleaner : IDbContextCleaner
    {
        private readonly PaymentDbContext _context;
        private readonly IHostingEnvironment _environment;

        public PaymentDbContextCleaner(IHostingEnvironment environment, PaymentDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        public Task CleanupAsync()
        {
            return Task.CompletedTask;
        }
    }
}