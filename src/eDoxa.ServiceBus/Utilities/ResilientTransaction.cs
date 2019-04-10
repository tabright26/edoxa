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

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        private ResilientTransaction(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ResilientTransaction NewInstance(DbContext context)
        {
            return new ResilientTransaction(context);
        }

        /// <summary>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        ///     See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
        /// </remarks>
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