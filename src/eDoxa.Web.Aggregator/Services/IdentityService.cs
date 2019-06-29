// Filename: IdentityService.cs
// Date Created: 2019-06-28
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
    public sealed class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<dynamic>> FetchUsersAsync()
        {
            var users = await _httpClient.GetStringAsync("http://192.168.0.100:5001/api/users");

            return JsonConvert.DeserializeObject<IReadOnlyCollection<dynamic>>(users);
        }
    }
}
