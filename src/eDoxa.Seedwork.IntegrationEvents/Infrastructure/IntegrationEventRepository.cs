// Filename: IntegrationEventRepository.cs
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
    public class IntegrationEventRepository : IIntegrationEventRepository
    {
        private readonly ServiceBusDbContext _context;

        public IntegrationEventRepository(DbConnection connection)
        {
            _context = new ServiceBusDbContext(
                new DbContextOptionsBuilder<ServiceBusDbContext>().UseSqlServer(connection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options
            );
        }

        public Task SaveIntegrationEventAsync(IntegrationEvent integrationEvent, DbTransaction transaction)
        {
            var model = new IntegrationEventModel(integrationEvent);

            _context.Database.UseTransaction(transaction);

            _context.IntegrationEvents.Add(model);

            return _context.SaveChangesAsync();
        }

        public Task MarkIntegrationEventAsPublishedAsync(IntegrationEvent integrationEvent)
        {
            var integrationEventLogEntry = _context.IntegrationEvents.Single(logEntry => logEntry.Id == integrationEvent.Id);

            integrationEventLogEntry.MarkAsPublished();

            _context.IntegrationEvents.Update(integrationEventLogEntry);

            return _context.SaveChangesAsync();
        }
    }
}
