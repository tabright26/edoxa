// Filename: JsonSerializerSettingsExtensions.cs
// Date Created: 2020-02-02
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.Json.Converters;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace eDoxa.Seedwork.Application.Json.Extensions
{
    public static class JsonSerializerSettingsExtensions
    {
        public static void IncludeCustomConverters(this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Converters.Add(new StringEnumConverter());
            settings.Converters.Add(new DecimalValueConverter());
            settings.Converters.Add(new TimestampConverter());
            settings.Converters.Add(new DurationConverter());
        }
    }
}
