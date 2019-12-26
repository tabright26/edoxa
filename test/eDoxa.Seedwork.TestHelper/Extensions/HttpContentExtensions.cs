// Filename: HttpContentExtensions.cs
// Date Created: 2019-12-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Converters;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent httpContent)
        {
            var formatter = new JsonMediaTypeFormatter();

            formatter.SerializerSettings.Converters.Add(new DecimalValueConverter());

            return await httpContent.ReadAsAsync<T>(
                new List<MediaTypeFormatter>
                {
                    formatter
                });
        }
    }
}
