// Filename: RequestLogRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Infrastructure.Repositories
{
    public class RequestLogRepository<TContext> : IRequestLogRepository
    where TContext : CustomDbContext
    {
        private readonly TContext _context;

        public RequestLogRepository(TContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public void Create(RequestLogEntry requestLog)
        {
            _context.RequestLogs.Add(requestLog);
        }

        public bool IdempotencyKeyExists([CanBeNull] string idempotencyKey)
        {
            return idempotencyKey != null && _context.RequestLogs.Any(logEntry => logEntry.IdempotencyKey == Guid.Parse(idempotencyKey));
        }
    }
}