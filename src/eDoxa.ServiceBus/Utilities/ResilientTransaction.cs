// Filename: ResilientTransaction.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.ServiceBus.Utilities
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

        public async Task ExecuteAsync(Func<Task> action)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(
                async () =>
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        await action();

                        transaction.Commit();
                    }
                }
            );
        }
    }
}