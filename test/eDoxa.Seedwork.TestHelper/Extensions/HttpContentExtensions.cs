// Filename: HttpContentExtensions.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Converters;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent httpContent)
        {
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings =
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            };

            formatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            formatter.SerializerSettings.Converters.Add(new DecimalValueConverter());
            formatter.SerializerSettings.Converters.Add(new TimestampConverter());
            formatter.SerializerSettings.Converters.Add(new DurationConverter());

            return await httpContent.ReadAsAsync<T>(
                new List<MediaTypeFormatter>
                {
                    formatter
                });
        }
    }
}
