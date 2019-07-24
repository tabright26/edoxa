// Filename: LeagueOfLegendsProxy.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Dtos;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends
{
    internal sealed class LeagueOfLegendsProxy : ILeagueOfLegendsProxy
    {
        private static readonly ICollection<DateTime> RequestTimestamps = new List<DateTime>();

        private readonly ILeagueOfLegendsService _leagueOfLegendsService;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<LeagueOfLegendsProxy> _logger;

        public LeagueOfLegendsProxy(ILeagueOfLegendsService leagueOfLegendsService, IDistributedCache distributedCache, ILogger<LeagueOfLegendsProxy> logger)
        {
            _leagueOfLegendsService = leagueOfLegendsService;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime)
        {
            var matchReferences = await _leagueOfLegendsService.GetMatchReferencesAsync(accountId, endTime, beginTime);

            this.TriggerRequestTimestamp();

            return matchReferences;
        }

        public async Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId)
        {
            var value = await _distributedCache.GetStringAsync(gameId);

            if (!string.IsNullOrWhiteSpace(value))
            {
                return JsonConvert.DeserializeObject<LeagueOfLegendsMatchDto>(value);
            }

            var match = await _leagueOfLegendsService.GetMatchAsync(gameId);

            this.TriggerRequestTimestamp();

            await _distributedCache.SetStringAsync(gameId, JsonConvert.SerializeObject(match));

            return match;
        }

        private void TriggerRequestTimestamp()
        {
            var timestamp = DateTime.UtcNow;

            RequestTimestamps.Add(timestamp);

            _logger.LogInformation($"{nameof(LeagueOfLegendsProxy)} - HTTP request sent to the League of Legends API at the following timestamp: {timestamp}.");
        }
    }
}
