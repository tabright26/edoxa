// Filename: CommandRepository.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

namespace eDoxa.Commands.Infrastructure.Repositories
{
    public partial class CommandRepository<TDbContext>
    where TDbContext : CustomDbContext
    {
        private readonly TDbContext _context;

        public CommandRepository(TDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public partial class CommandRepository<TDbContext> : ICommandRepository
    where TDbContext : CustomDbContext
    {
        public void Create(CommandLogEntry logEntry)
        {
            _context.Logs.Add(logEntry);
        }

        public bool IdempotencyKeyExists([CanBeNull] string idempotencyKey)
        {
            return idempotencyKey != null && _context.Logs.Any(logEntry => logEntry.IdempotencyKey == Guid.Parse(idempotencyKey));
        }
    }
}