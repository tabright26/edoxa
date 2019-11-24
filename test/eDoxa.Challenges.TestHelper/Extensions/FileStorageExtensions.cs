// Filename: FileStorageExtensions.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;

using eDoxa.Challenges.Api.Infrastructure.Data.Storage;

using Newtonsoft.Json;

namespace eDoxa.Challenges.TestHelper.Extensions
{
    public static class FileStorageExtensions
    {
        public static T DeserializeJsonFile<T>(this FileStorage _, string path)
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, path);

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
