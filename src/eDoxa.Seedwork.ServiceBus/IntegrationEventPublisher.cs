// Filename: IntegrationEventPublisher.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Data.Common;
using System.Threading.Tasks;

using eDoxa.Seedwork.ServiceBus.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eDoxa.Seedwork.ServiceBus
{
    public class IntegrationEventPublisher<TDbContext> : IIntegrationEventPublisher
    where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IIntegrationEventRepository _integrationEventRepository;

        public IntegrationEventPublisher(
            TDbContext dbContext,
            IServiceBusPublisher serviceBusPublisher,
            Func<DbConnection, IIntegrationEventRepository> integrationEventRepository
        )
        {
            _dbContext = dbContext;
            _serviceBusPublisher = serviceBusPublisher;
            _integrationEventRepository = integrationEventRepository(_dbContext.Database.GetDbConnection());
        }

        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            await ResilientTransaction.NewInstance(_dbContext)
                .ExecuteAsync(
                    async () =>
                    {
                        await _dbContext.SaveChangesAsync();

                        await _integrationEventRepository.SaveIntegrationEventAsync(
                            integrationEvent,
                            _dbContext.Database.CurrentTransaction.GetDbTransaction()
                        );
                    }
                );

            _serviceBusPublisher.Publish(integrationEvent);

            await _integrationEventRepository.MarkIntegrationEventAsPublishedAsync(integrationEvent);
        }
    }
}
