// Filename: LeagueOfLegendsService.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Dtos;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends
{
    internal sealed class LeagueOfLegendsService : ILeagueOfLegendsService
    {
        private readonly HttpClient _httpClient;

        public LeagueOfLegendsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LeagueOfLegendsMatchReferenceDto[]> GetMatchReferencesAsync(string accountId, DateTime endTime, DateTime beginTime)
        {
            var builder = new UriBuilder($"https://na1.api.riotgames.com/lol/match/v4/matchlists/by-account/{accountId}")
            {
                Port = -1
            };

            var query = HttpUtility.ParseQueryString(builder.Query);

            query.Add("endTime", ((DateTimeOffset) endTime.ToUniversalTime()).ToUnixTimeMilliseconds().ToString());

            query.Add("beginTime", ((DateTimeOffset) beginTime.ToUniversalTime()).ToUnixTimeMilliseconds().ToString());

            builder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LeagueOfLegendsMatchReferenceDto[]>(json);
            }

            throw new HttpRequestException(json);
        }

        public async Task<LeagueOfLegendsMatchDto> GetMatchAsync(string gameId)
        {
            var builder = new UriBuilder($"https://na1.api.riotgames.com/lol/match/v4/matches/{gameId}")
            {
                Port = -1
            };

            var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());

            var response = await _httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LeagueOfLegendsMatchDto>(json);
            }

            throw new HttpRequestException(json);
        }
    }
}
