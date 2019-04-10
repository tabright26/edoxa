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

using eDoxa.ServiceBus.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace eDoxa.ServiceBus
{
    public class IntegrationEventService<TDbContext> : IIntegrationEventService
    where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private readonly IEventBusService _eventBusService;
        private readonly IIntegrationEventLogRepository _integrationEventLogRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntegrationEventService{TDbContext}" /> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext" />.</param>
        /// <param name="eventBusService">The <see cref="IEventBusService" />.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public IntegrationEventService(
            TDbContext context,
            IEventBusService eventBusService,
            Func<DbConnection, IIntegrationEventLogRepository> repositoryFactory)
        {
            repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBusService = eventBusService ?? throw new ArgumentNullException(nameof(eventBusService));
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