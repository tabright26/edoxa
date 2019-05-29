// Filename: StubConvert.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;
using System.Runtime.InteropServices;

using Newtonsoft.Json;

namespace eDoxa.Arena.Tests.Stubs
{
    public static class StubConvert
    {
        public static T DeserializeObject<T>(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = path.Replace("/", @"\");
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + path));
        }
    }
}
