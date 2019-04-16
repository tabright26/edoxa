// Filename: IntegrationEventLogRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eDoxa.ServiceBus
{
    public class IntegrationEventLogRepository : IIntegrationEventLogRepository
    {
        private readonly IntegrationEventLogDbContext _context;

        public IntegrationEventLogRepository(DbConnection connection)
        {
            _context = new IntegrationEventLogDbContext(
                new DbContextOptionsBuilder<IntegrationEventLogDbContext>()
                    .UseSqlServer(connection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options
            );
        }

        public Task SaveIntegrationEventAsync(IntegrationEvent integrationEvent, DbTransaction transaction)
        {
            var logEntry = new IntegrationEventLogEntry(integrationEvent);

            _context.Database.UseTransaction(transaction);

            _context.IntegrationEventLogs.Add(logEntry);

            return _context.SaveChangesAsync();
        }

        public Task MarkIntegrationEventAsPublishedAsync(IntegrationEvent integrationEvent)
        {
            var integrationEventLogEntry = _context.IntegrationEventLogs.Single(logEntry => logEntry.Id == integrationEvent.Id);

            integrationEventLogEntry.MarkAsPublished();

            _context.IntegrationEventLogs.Update(integrationEventLogEntry);

            return _context.SaveChangesAsync();
        }
    }
}