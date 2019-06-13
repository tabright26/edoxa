// Filename: MapperExtensions.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Testing.TestServer.Extensions
{
    public static class MapperExtensions
    {
        [CanBeNull]
        public static T Deserialize<T>(this IMapper mapper, object source)
        {
            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(
                    mapper.Map<T>(source),
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                )
            );
        }
    }
}
