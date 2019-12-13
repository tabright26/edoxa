using System;

using eDoxa.Grpc.Protos.CustomTypes;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application
{
    public class DecimalValueConverter : JsonConverter<DecimalValue>
    {
        public override void WriteJson(JsonWriter writer, DecimalValue value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToDecimal());
        }

        public override DecimalValue ReadJson(
            JsonReader reader,
            Type objectType,
            DecimalValue existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            return DecimalValue.FromDecimal((decimal) reader.Value!);
        }
    }
}
