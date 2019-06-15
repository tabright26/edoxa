// Filename: ObjectExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static string DumbAsJson<T>(this T obj, bool console = false)
        where T : class
        {
            var json = JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );

            if (console)
            {
                Console.WriteLine($"{obj.GetType().FullName} object as json:");

                Console.WriteLine(json);
            }

            return json;
        }
    }
}
