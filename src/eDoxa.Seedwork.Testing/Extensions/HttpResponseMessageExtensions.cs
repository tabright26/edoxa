﻿// Filename: HttpResponseMessageExtensions.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        [ItemCanBeNull]
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}