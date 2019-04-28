// Filename: ICommandRepository.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Infrastructure.Repositories
{
    public interface ICommandRepository : IRepository<LogEntry>
    {
        void Create(LogEntry logEntry);

        bool IdempotencyKeyExists([CanBeNull] string idempotencyKey);
    }
}