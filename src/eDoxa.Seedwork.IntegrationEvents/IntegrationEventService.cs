// Filename: IntegrationEventService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Data.Common;
using System.Threading.Tasks;

using eDoxa.Seedwork.IntegrationEvents.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public class IntegrationEventService<TDbContext> : IIntegrationEventService
    where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly IEventBusService _eventBusService;
        private readonly IIntegrationEventLogRepository _integrationEventLogRepository;

        public IntegrationEventService(
            TDbContext context,
            IEventBusService eventBusService,
            Func<DbConnection, IIntegrationEventLogRepository> repositoryFactory)
        {
            _context = context;
            _eventBusService = eventBusService;
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

            _eventBusService.Publish(integrationEvent);

            await _integrationEventLogRepository.MarkIntegrationEventAsPublishedAsync(integrationEvent);
        }
    }
}