// Filename: PaymentDbContext.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Payment.Infrastructure
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
    }
}
