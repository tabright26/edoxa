// Filename: HttpResponseMessageExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T?> DeserializeAsync<T>(this HttpResponseMessage response)
        where T : class
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
