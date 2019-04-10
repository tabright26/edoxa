// Filename: IRequestLogRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Seedwork.Infrastructure.Repositories
{
    public interface IRequestLogRepository : IRepository<RequestLogEntry>
    {
        void Create(RequestLogEntry requestLog);

        bool IdempotencyKeyExists(string idempotencyKey);
    }
}