// Filename: JsonFileConvert.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers
{
    public static class JsonFileConvert
    {
        public static T DeserializeObject<T>(string path)
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
