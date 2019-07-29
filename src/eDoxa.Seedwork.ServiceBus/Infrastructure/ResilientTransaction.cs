// Filename: ResilientTransaction.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.ServiceBus.Infrastructure
{
    public class ResilientTransaction
    {
        private readonly DbContext _context;

        private ResilientTransaction(DbContext context)
        {
            _context = context;
        }

        public static ResilientTransaction NewInstance(DbContext context)
        {
            return new ResilientTransaction(context);
        }

        public async Task ExecuteAsync(Func<Task> operationAsync)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(
                async () =>
                {
                    using var transaction = _context.Database.BeginTransaction();

                    await operationAsync();

                    transaction.Commit();
                }
            );
        }
    }
}
