// Filename: TimestampConverter.cs
// Date Created: 2020-01-12
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using Google.Protobuf.WellKnownTypes;

using Newtonsoft.Json;

using Type = System.Type;

namespace eDoxa.Seedwork.Application.Converters
{
    public sealed class DurationConverter : JsonConverter<Duration?>
    {
        public override void WriteJson(JsonWriter writer, Duration? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToTimeSpan().Seconds);
            }
        }

        public override Duration? ReadJson(
            JsonReader reader,
            Type objectType,
            Duration? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            return reader.Value != null ? Duration.FromTimeSpan(TimeSpan.FromSeconds(Convert.ToInt64(reader.Value))) : null;
        }
    }
}
