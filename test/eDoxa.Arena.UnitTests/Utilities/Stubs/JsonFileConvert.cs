// Filename: JsonFileConvert.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;

using Newtonsoft.Json;

namespace eDoxa.Arena.UnitTests.Utilities.Stubs
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
