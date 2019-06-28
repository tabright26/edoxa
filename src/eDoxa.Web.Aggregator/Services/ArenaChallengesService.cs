// Filename: ArenaChallengesService.cs
// Date Created: 2019-06-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace eDoxa.Web.Aggregator.Services
{
    public class ArenaChallengesService : IArenaChallengesService
    {
        private readonly HttpClient _httpClient;

        public ArenaChallengesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<dynamic>> FetchChallenges()
        {
            // TODO: Create an option class with all the URLs.
            var challenges = await _httpClient.GetStringAsync("http://192.168.0.100:5003/api/challenges");

            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(challenges);
        }
    }
}
