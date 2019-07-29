// Filename: IntegrationEventLogRepository.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public class IntegrationEventLogRepository : IIntegrationEventLogRepository
    {
        private readonly IntegrationEventDbContext _context;

        public IntegrationEventLogRepository(DbConnection connection)
        {
            _context = new IntegrationEventDbContext(
                new DbContextOptionsBuilder<IntegrationEventDbContext>().UseSqlServer(connection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options
            );
        }

        public Task SaveIntegrationEventAsync(IntegrationEvent integrationEvent, DbTransaction transaction)
        {
            var logEntry = new IntegrationEventLogEntry(integrationEvent);

            _context.Database.UseTransaction(transaction);

            _context.Logs.Add(logEntry);

            return _context.SaveChangesAsync();
        }

        public Task MarkIntegrationEventAsPublishedAsync(IntegrationEvent integrationEvent)
        {
            var integrationEventLogEntry = _context.Logs.Single(logEntry => logEntry.Id == integrationEvent.Id);

            integrationEventLogEntry.MarkAsPublished();

            _context.Logs.Update(integrationEventLogEntry);

            return _context.SaveChangesAsync();
        }
    }
}
