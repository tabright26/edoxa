// Filename: HttpClientExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing.TestServer.Extensions
{
    public static class HttpClientExtensions
    {
        // TODO: Must be a JwtToken. (temporairy)
        public static HttpClient DefaultRequestHeaders(this HttpClient httpClient, IEnumerable<Claim> claims)
        {
            var json = JsonConvert.SerializeObject(claims.ToDictionary(claim => claim.Type, claim => claim.Value));

            httpClient.DefaultRequestHeaders.Add(nameof(Claim), json);

            return httpClient;
        }
    }
}
