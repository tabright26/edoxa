// Filename: HttpContextExtensions.cs
// Date Created: 2020-02-12
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Json.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> CustomPostAsJsonAsync<T>(this HttpClient httpClient, string requestUri, T value)
        where T : class
        {
            var formatter = new JsonMediaTypeFormatter();

            formatter.SerializerSettings.IncludeCustomConverters();
            
            return await httpClient.PostAsJsonAsync(requestUri, JToken.FromObject(value, JsonSerializer.Create(formatter.SerializerSettings)));
        }
    }
}
