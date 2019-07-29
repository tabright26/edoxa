// Filename: IntegrationEventService.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Data.Common;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public class IntegrationEventService<TDbContext> : IIntegrationEventService
    where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IIntegrationEventLogRepository _integrationEventLogRepository;

        public IntegrationEventService(
            TDbContext context,
            IServiceBusPublisher serviceBusPublisher,
            Func<DbConnection, IIntegrationEventLogRepository> repositoryFactory
        )
        {
            _context = context;
            _serviceBusPublisher = serviceBusPublisher;
            _integrationEventLogRepository = repositoryFactory(_context.Database.GetDbConnection());
        }

        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            await ResilientTransaction.NewInstance(_context)
                .ExecuteAsync(
                    async () =>
                    {
                        await _context.SaveChangesAsync();

                        await _integrationEventLogRepository.SaveIntegrationEventAsync(
                            integrationEvent,
                            _context.Database.CurrentTransaction.GetDbTransaction()
                        );
                    }
                );

            _serviceBusPublisher.Publish(integrationEvent);

            await _integrationEventLogRepository.MarkIntegrationEventAsPublishedAsync(integrationEvent);
        }
    }
}
