// Filename: TimestampConverter.cs
// Date Created: 2020-01-12
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using Google.Protobuf.WellKnownTypes;

using Newtonsoft.Json;

using Type = System.Type;

namespace eDoxa.Seedwork.Application.Json.Converters
{
    public sealed class TimestampConverter : JsonConverter<Timestamp?>
    {
        public override void WriteJson(JsonWriter writer, Timestamp? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToDateTimeOffset().ToUnixTimeSeconds());
            }
        }

        public override Timestamp? ReadJson(
            JsonReader reader,
            Type objectType,
            Timestamp? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            return reader.Value != null ? DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(reader.Value)).ToTimestamp() : null;
        }
    }
}
