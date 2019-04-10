// Filename: ChallengeSyncingBackgroundService.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.BackgroundTasks.IntegrationEvents;
using eDoxa.Challenges.BackgroundTasks.Settings;
using eDoxa.ServiceBus;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.BackgroundTasks.Services
{
    public class ChallengeSynchronizingBackgroundService : BackgroundService
    {
        private readonly IOptionsMonitor<ChallengeSynchronizingSettings> _monitor;
        private readonly ILogger _logger;
        private readonly IEventBusService _eventService;

        public ChallengeSynchronizingBackgroundService(ILoggerFactory loggerFactory, IEventBusService eventService, IOptionsMonitor<ChallengeSynchronizingSettings> monitor)
        {
            _monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
            _logger = loggerFactory?.CreateLogger<ChallengeSynchronizingBackgroundService>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ChallengeSynchronizingBackgroundService)} is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var settings = _monitor.CurrentValue;

                _logger.LogInformation(settings.ToString());

                if (settings.Enabled)
                {
                    _logger.LogInformation($"{nameof(ChallengeSynchronizingBackgroundService)} is running.");

                    _eventService.Publish(new ChallengesSynchronizedIntegrationEvent());
                }
                else
                {
                    _logger.LogInformation($"{nameof(ChallengeSynchronizingBackgroundService)} is disabled.");
                }

                await Task.Delay(TimeSpan.FromSeconds(settings.Delay), stoppingToken);
            }

            _logger.LogInformation($"{nameof(ChallengeSynchronizingBackgroundService)} is stopping");

            await Task.CompletedTask;
        }
    }
}