// Filename: DecimalValueConverter.cs
// Date Created: 2019-12-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Grpc.Protos.CustomTypes;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Converters
{
    public sealed class DecimalValueConverter : JsonConverter<DecimalValue?>
    {
        public override void WriteJson(JsonWriter writer, DecimalValue? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToDecimal());
            }
        }

        public override DecimalValue? ReadJson(
            JsonReader reader,
            Type objectType,
            DecimalValue? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            if (reader.Value is double value)
            {
                return DecimalValue.FromDecimal(new decimal(value));
            }
            else
            {
                return null;
            }
        }
    }
}
