// Filename: ICommandRepository.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Infrastructure.Models;

namespace eDoxa.Commands.Infrastructure.Repositories
{
    public interface ICommandRepository : IRepository<LogEntry>
    {
        void Create(CommandLogEntry logEntry);

        bool IdempotencyKeyExists(Guid idempotencyKey);
    }
}